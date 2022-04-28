using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 hookShotPosition;
    private bool groundedPlayer;
    private bool timeSlowed;
    public static float playerSpeed;
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
    public Transform rightElbow;
    public Transform hookShotTransform;
    private State state;
    AudioSource audioSource;
    public AudioClip zapped;
    public AudioClip hooked;
    public AudioClip warp;
    public Image slowTimeBar;
    public Animator animator;
    public LayerMask ignoreGrapple;

    // States
    private enum State
    {
        Normal, HookShotFlyingPlayer, HookShotThrown
    }

    IEnumerator ChangeScenes()
    {
        BGMusic.audioSource.Stop();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(4);
    }

    // Called on object Awake in Scene
    private void Awake()
    {
        playerSpeed = 12f;
        currentTimeSlowAmount = maxTimeSlowAmount;

        state = State.Normal;
        cameraTransform = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
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
        if (zapAction.triggered && PauseMenu.gameIsPaused == false)
        {
            GameObject zap = GameObject.Instantiate(zapBall, zapBarrelTip.position, Quaternion.identity);
            audioSource.PlayOneShot(zapped);
        }
    }

    // Slows time by half when pressing F, can only be activated with max current timeslow amount
    private void HandleTimeSlowTrigger()
    {
        {
            if (slowTimeAction.triggered && !timeSlowed && currentTimeSlowAmount == 5.0f && PauseMenu.gameIsPaused == false)
            {
                timeSlowed = true;
                audioSource.PlayOneShot(warp, 1.35f);
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
        if (timeSlowed && PauseMenu.gameIsPaused == false && (PlayerPrefs.GetInt("TimeSlowCheat") == 1 ? false:true))
        {
            Time.timeScale = 0.5f;
            currentTimeSlowAmount = Mathf.Clamp(currentTimeSlowAmount -= Time.deltaTime * 2, 0.0f, 5.0f);
            slowTimeBar.fillAmount = Mathf.Clamp(slowTimeBar.fillAmount -= Time.deltaTime / 2.5f, 0.0f, 1.0f);
        }
        else if (!timeSlowed && PauseMenu.gameIsPaused == false && (PlayerPrefs.GetInt("TimeSlowCheat") == 1 ? false:true))
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

    private void HandleTimeSlowCheat()
    {
        if (timeSlowed && PauseMenu.gameIsPaused == false && (PlayerPrefs.GetInt("TimeSlowCheat") == 0 ? false:true))
        {
            Time.timeScale = 0.5f;
        }
        else if (!timeSlowed && PauseMenu.gameIsPaused == false && (PlayerPrefs.GetInt("TimeSlowCheat") == 0 ? false:true))
        {
            Time.timeScale = 1.0f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TimeCore")
        {
            TimeCore timeCore = other.GetComponent<TimeCore>();
            timeCore.HandleTimeCoreInteraction();
            StartCoroutine(ChangeScenes());
        }
        if (other.tag == "TimeCore" && SceneManager.GetActiveScene().buildIndex == 3)
        {
            GlobalSave.Instance.portalThreeActive = false;
        }
        if (other.tag == "TimeCore" && SceneManager.GetActiveScene().buildIndex == 2)
        {
            GlobalSave.Instance.portalTwoActive = false;
        }
        if (other.tag == "TimeCore" && SceneManager.GetActiveScene().buildIndex == 1)
        {
            GlobalSave.Instance.portalOneActive = false;
        }
        if (other.tag == "PlayerEnd")
        {
            RaceWin.Instance.PlayerWin();
        }
        if (other.tag == "PreTel")
        {
            SceneManager.LoadScene(1);
        }
        if (other.tag == "CasTel")
        {
            SceneManager.LoadScene(2);
        }
        if (other.tag == "CityTel")
        {
            SceneManager.LoadScene(3);
        }
    }

    // Rotates Player
    private void HandleRotations()
    {
        // Rotate player towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation =  Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Rotates the gun up and down based off camera
        Quaternion rightElbowTargetRotation = Quaternion.Euler(cameraTransform.eulerAngles);
        rightElbow.rotation = Quaternion.Lerp(rightElbow.rotation, rightElbowTargetRotation, rotationSpeed * Time.deltaTime);
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
        if (move.x > 0 || move.x < 0 || move.z > 0 || move.z < 0)
        {
            animator.SetBool("Running", true);
            animator.SetBool("Idleing", false);
        }
        else
        {
            animator.SetBool("Running", false);
            animator.SetBool("Idleing", true);
        }

        // Jump
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.SetTrigger("Jump");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        
        // Applies Gravity to player
        controller.Move(playerVelocity * Time.deltaTime);
    }
    // Shoots a Raycast and creates a point for the player to travel to
    private void HandleHookShotStart()
    {
        if(grappleAction.triggered && PauseMenu.gameIsPaused == false)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, grappleRange, ~ignoreGrapple))
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
        hookShotTransform.position = hookBarrelTip.position;
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
        animator.SetBool("Idleing", true);
        hookShotTransform.LookAt(hookShotPosition);
        hookShotTransform.position = hookBarrelTip.position;
        
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
                HandleTimeSlowCheat();
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
                HandleTimeSlowCheat();
                HandleTimeSlowTrigger();
            break;

            case State.HookShotFlyingPlayer:
                CloseGame();
                HandleZap();
                HandleTimeSlow();
                HandleTimeSlowCheat();
                HandleTimeSlowTrigger();
                HandleRotations();
                HandelHookShotMovement();
                HandleSlowTimeBarColor();
                break;
        }
    }
}
