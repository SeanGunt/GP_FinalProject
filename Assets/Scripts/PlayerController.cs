using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool timeSlowed;
    private float playerSpeed = 12.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float rotationSpeed = 50f;
    private float maxTimeSlowAmount = 5f;
    private float currentTimeSlowAmount;
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
    private State state;
    private Vector3 hookShotPosition;

    private enum State
    {
        Normal, HookShotFlyingPlayer
    }

    private void Awake()
    {
        currentTimeSlowAmount = maxTimeSlowAmount;

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


    // Zap Projectile insantiates at barrel tip, sets target at hit point (center of screen)
    private void HandleZap()
    {
        if (zapAction.triggered)
        {
            GameObject zap = GameObject.Instantiate(zapBall, zapBarrelTip.position, Quaternion.identity);
        }
    }

    // Slows time by half when pressing F, can only be activated with max current timeslow amount
    private void HandleTimeSlowTrigger()
    {
        {
            if (slowTimeAction.triggered && !timeSlowed && currentTimeSlowAmount == 5.0f)
            {
                timeSlowed = true;
            }
            else if (slowTimeAction.triggered)
            {
                timeSlowed = false;
            }
        }
    }
    private void HandleTimeSlow()
    {
        // Meter for time slow that ticks down at 1 per second, the meter ticking down is multiplied by two since time is running at half speed
        if (timeSlowed)
        {
            Time.timeScale = 0.5f;
            currentTimeSlowAmount = Mathf.Clamp(currentTimeSlowAmount -= Time.deltaTime * 2, 0.0f, 5.0f);
        }
        else
        {
            Time.timeScale = 1.0f;
            currentTimeSlowAmount = Mathf.Clamp(currentTimeSlowAmount += Time.deltaTime, 0.0f, 5.0f);
        }
        if (currentTimeSlowAmount == 0)
        {
            timeSlowed = false;
        }
        //Debug.Log(currentTimeSlowAmount);
    }

    private void HandleCharacterRotation()
    {
        // Rotate player towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation =  Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Rotates the gun up and down based off camera
        Quaternion gunRotationTarget = Quaternion.Euler(cameraTransform.eulerAngles);
        gunModel.rotation = Quaternion.Lerp(gunModel.rotation, gunRotationTarget, rotationSpeed * Time.deltaTime);
    }

    private void HandleCharacterMovement()
    {
        // Moves the player
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

        // Jump
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    // Shoots a Raycast and creates a point for the player to travel to
    private void HandleHookShotStart()
    {
        if( grappleAction.triggered)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit))
            {
                hookShotPosition = hit.point;
                state = State.HookShotFlyingPlayer;
            }
        }
    }
    // The actual movement that happens during the hookshot
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
        // States
        switch (state)
        {
            default:
            case State.Normal:
                HandleZap();
                HandleTimeSlow();
                HandleTimeSlowTrigger();
                HandleCharacterMovement();
                HandleCharacterRotation();
                HandleHookShotStart();
            break;
            case State.HookShotFlyingPlayer:
                HandleZap();
                HandleTimeSlow();
                HandleTimeSlowTrigger();
                HandleCharacterRotation();
                HandelHookShotMovement();
            break;
        }
    }
}
