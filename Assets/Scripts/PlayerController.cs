using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool timeSlowed;
    private float playerSpeed = 6.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float rotationSpeed = 50f;
    private float range = 100.0f;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction zapAction;
    private InputAction grappleAction;
    private InputAction slowTimeAction;
    public GameObject zapBall;
    public GameObject hookPrefab;
    private Transform cameraTransform;
    public Transform zapBarrelTip;
    public Transform hookBarrelTip;
    public Transform gunModel;
    public Transform debugHitPointTransform;
    private State state;
    private Vector3 hookShotPosition;

    private enum State
    {
        Normal, HookShotFlyingPlayer
    }

    private void Awake()
    {
        state = State.Normal;
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        zapAction = playerInput.actions["Zap"];
        grappleAction = playerInput.actions["Grapple"];
        slowTimeAction = playerInput.actions["SlowTime"];

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Starts Actions//
    private void OnEnable()
    {
        zapAction.performed += _ => Zap();
    }
    //Ends Actions//
    private void OnDisable()
    {
        zapAction.performed -= _ => Zap();
    }

    //Zap Projectile insantiates at barrel tip, sets target at hit point (center of screen)//
    private void Zap()
    {
        RaycastHit hit;
        GameObject zap = GameObject.Instantiate(zapBall, zapBarrelTip.position, Quaternion.identity);
        ZapController zapController = zap.GetComponent<ZapController>();
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, range))
        {
            zapController.target = hit.point;
        }
        else
        {
            zapController.target = cameraTransform.position + cameraTransform.forward * range;
        }
        
    }

    //Slows time by half when pressing F
    private void HandleTimeSlow()
    {
        if (slowTimeAction.triggered)
        {
            if (!timeSlowed)
            {
                Time.timeScale = 0.5f;
                timeSlowed = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                timeSlowed = false;
            }
        }
    }

    private void HandleCharacterRotation()
    {
        //Rotate player towards camera direction//
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation =  Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //Rotates the gun up and down based off camera//
        Quaternion gunRotationTarget = Quaternion.Euler(cameraTransform.eulerAngles);
        gunModel.rotation = Quaternion.Lerp(gunModel.rotation, gunRotationTarget, rotationSpeed * Time.deltaTime);
    }

    private void HandleCharacterMovement()
    {
        //Moves the player//
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        //Jump//
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleHookShotStart()
    {
        if( grappleAction.triggered)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit))
            {
                debugHitPointTransform.position = hit.point;
                hookShotPosition = hit.point;
                state = State.HookShotFlyingPlayer;
            }
        }
    }

    private void HandelHookShotMovement()
    {
        Vector3 hookShotDir = (hookShotPosition - transform.position).normalized;
        float hookShotSpeed = Vector3.Distance(transform.position, hookShotPosition);
        float hookshotSpeedMultiplyer = 2f;
        controller.Move(hookShotDir * hookShotSpeed * hookshotSpeedMultiplyer* Time.deltaTime);

        float reachedHookShotPositionDistance = 2f;
        if (Vector3.Distance(transform.position, hookShotPosition) < reachedHookShotPositionDistance)
        {
            state = State.Normal;
        }
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                HandleTimeSlow();
                HandleCharacterMovement();
                HandleCharacterRotation();
                HandleHookShotStart();
            break;
            case State.HookShotFlyingPlayer:
                HandleTimeSlow();
                HandleCharacterRotation();
                HandelHookShotMovement();
            break;
        }
    }
}
