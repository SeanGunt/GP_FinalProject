using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 6.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float rotationSpeed = 50f;
    private float range = 100.0f;
    private Transform cameraTransform;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction zapAction;
    private InputAction grappleAction;
    public GameObject zapBall;
    public Transform gunBarrelTip;
    public LayerMask environment;
    

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        zapAction = playerInput.actions["Zap"];
        grappleAction = playerInput.actions["Grapple"];

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Stats Actions//
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
        GameObject zap = GameObject.Instantiate(zapBall, gunBarrelTip.position, Quaternion.identity);
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

    void Update()
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

        //Rotate player towards camera direction//
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation =  Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
