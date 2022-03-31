using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 hookShotPosition;
    private bool groundedPlayer;
    private bool timeSlowed;
    private float playerSpeed = 12.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float rotationSpeed = 50f;
    private float maxTimeSlowAmount = 5f;
    private float currentTimeSlowAmount;
    private float hookShotSize;
    private float grappleRange = 35f;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction zapAction;
    private InputAction grappleAction;
    private InputAction slowTimeAction;
    private InputAction closeGameAction;
    public GameObject zapBall;
    public GameObject hookPrefab;
    public static GameObject hook;
    private Transform cameraTransform;
    public Transform zapBarrelTip;
    public Transform hookBarrelTip;
    public Transform gunModel;
    public Transform hookShotTransform;
    private State state;
    AudioSource audioSource;
    public AudioClip zapped;
    public AudioClip jump;
    public AudioClip hooked;
    public AudioClip warp;
    public Image slowTimeBar; 

    // States
    private enum State
    {
        Normal, HookShotFlyingPlayer, HookShotThrown
    }

    // Called on object Awake in Scene
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        currentTimeSlowAmount = maxTimeSlowAmount;

        state = State.Normal;
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        hookShotTransform.gameObject.SetActive(false);

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        zapAction = playerInput.actions["Zap"];
        grappleAction = playerInput.actions["Grapple"];
        slowTimeAction = playerInput.actions["SlowTime"];
        closeGameAction = playerInput.actions["CloseGame"];

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Zap Projectile insantiates at barrel tip, sets target at hit point (center of screen)
    private void HandleZap()
    {
        if (zapAction.triggered)
        {
            GameObject zap = GameObject.Instantiate(zapBall, zapBarrelTip.position, Quaternion.identity);
            audioSource.PlayOneShot(zapped);
        }
    }

    // Slows time by half when pressing F, can only be activated with max current timeslow amount
    private void HandleTimeSlowTrigger()
    {
        {
            if (slowTimeAction.triggered && !timeSlowed && currentTimeSlowAmount == 5.0f)
            {
                timeSlowed = true;
                audioSource.PlayOneShot(warp);
            }
            else if (slowTimeAction.triggered)
            {
                timeSlowed = false;
            }
        }
    }
    // Time Slow mechanic
    private void HandleTimeSlow()
    {
        // Meter for time slow that ticks down at 1 per second, the meter ticking down is multiplied by two since time is running at half speed
        if (timeSlowed)
        {
            Time.timeScale = 0.5f;
            currentTimeSlowAmount = Mathf.Clamp(currentTimeSlowAmount -= Time.deltaTime * 2, 0.0f, 5.0f);
            slowTimeBar.fillAmount = Mathf.Clamp(slowTimeBar.fillAmount -= Time.deltaTime / 2.5f, 0.0f, 1.0f);
        }
        else
        {
            Time.timeScale = 1.0f;
            currentTimeSlowAmount = Mathf.Clamp(currentTimeSlowAmount += Time.deltaTime, 0.0f, 5.0f);
            slowTimeBar.fillAmount = Mathf.Clamp(slowTimeBar.fillAmount += Time.deltaTime / 5f, 0.0f, 1.0f);
        }
        if (currentTimeSlowAmount == 0)
        {
            timeSlowed = false;
        }
    }

    // Changes color of slow bar based on the amount of meter;
    private void HandleSlowTimeBarColor()
    {
        if(currentTimeSlowAmount == 5.0f)
        {
            slowTimeBar.color = Color.green;
        }
        else
        {
            slowTimeBar.color = Color.blue;
        }
    }

    // Rotates Player
    private void HandleRotations()
    {
        // Rotate player towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation =  Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Rotates the gun up and down based off camera
        Quaternion gunRotationTarget = Quaternion.Euler(cameraTransform.eulerAngles);
        gunModel.rotation = Quaternion.Lerp(gunModel.rotation, gunRotationTarget, rotationSpeed * Time.deltaTime);
    }

    // Character Movement
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
            audioSource.PlayOneShot(jump);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        
        // Applies Gravity to player
        controller.Move(playerVelocity * Time.deltaTime);
    }
    // Shoots a Raycast and creates a point for the player to travel to
    private void HandleHookShotStart()
    {
        if( grappleAction.triggered)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, grappleRange))
            {
                hook = Instantiate(hookPrefab, hit.point, Quaternion.identity);
                hookShotPosition = hit.point;
                hookShotSize = 0f;
                hookShotTransform.gameObject.SetActive(true);
                hookShotTransform.localScale = Vector3.zero;
                state = State.HookShotThrown;
                audioSource.PlayOneShot(hooked);
            }
        }
    }
    // Handles the line that comes out of the gun
    private void HandleHookShotThrow()
    {
        hookShotTransform.LookAt(hookShotPosition);

        float hookShotThrowSpeed = 70f;
        hookShotSize += hookShotThrowSpeed * Time.deltaTime;
        hookShotTransform.localScale = new Vector3(1,1,hookShotSize);

        if (hookShotSize >= Vector3.Distance(transform.position, hookShotPosition))
        {
            state = State.HookShotFlyingPlayer;
        }
    }
    // The actual movement that happens during the hookshot
    private void HandelHookShotMovement()
    {
        hookShotTransform.LookAt(hookShotPosition);
        Vector3 hookShotDir = (hookShotPosition - transform.position).normalized;
        float hookShotSpeedMin = 10f;
        float hookShotSpeedMax = 40f;
        float hookShotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookShotPosition), hookShotSpeedMin, hookShotSpeedMax);
        float hookshotSpeedMultiplyer = 2f;

        controller.Move(hookShotDir * hookShotSpeed * hookshotSpeedMultiplyer* Time.deltaTime);

        float reachedHookShotPositionDistance = 2f;
        if (Vector3.Distance(transform.position, hookShotPosition) < reachedHookShotPositionDistance)
        {
            Destroy(hook);
            state = State.Normal;
            ResetGravity();
            StopHookShot();
        }

        if (jumpAction.triggered)
        {
            Destroy(hook);
            state = State.Normal;
            ResetGravity();
            StopHookShot();
        }
    }

    // Sets line from hookshot to inactive
    private void StopHookShot()
    {
        hookShotTransform.gameObject.SetActive(false);
    }

    // Resets Gravity
    private void ResetGravity()
    {
        playerVelocity.y = 0;
    }

    // Closes Game
    private void CloseGame()
    {
        if (closeGameAction.triggered)
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }

    void Update()
    {
        // States
        switch (state)
        {
            default:
            case State.Normal:
                CloseGame();
                HandleZap();
                HandleTimeSlow();
                HandleTimeSlowTrigger();
                HandleCharacterMovement();
                HandleRotations();
                HandleHookShotStart();
                HandleSlowTimeBarColor();
            break;

            case State.HookShotThrown:
                CloseGame();
                HandleCharacterMovement();
                HandleRotations();
                HandleHookShotThrow();
                HandleSlowTimeBarColor();
                HandleTimeSlow();
                HandleTimeSlowTrigger();
            break;

            case State.HookShotFlyingPlayer:
                CloseGame();
                HandleZap();
                HandleTimeSlow();
                HandleTimeSlowTrigger();
                HandleRotations();
                HandelHookShotMovement();
                HandleSlowTimeBarColor();
                break;
        }
    }
}
