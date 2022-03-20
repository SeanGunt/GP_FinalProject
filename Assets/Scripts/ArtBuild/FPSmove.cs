using UnityEngine;
using UnityEngine.InputSystem;

public class FPSmove : MonoBehaviour
{
    Rigidbody rb;
    private InputAction moveAction;
    private PlayerInput playerInput;
    private InputAction moveCameraWithMouseAction;
    private InputAction endGameAction;
    public Transform cam;
    CharacterController characterController;
    float sensitivtyX = 5f;
    float sensitivtyY = 0.1f;
    float mouseX, mouseY;
    float xRotation = 0f;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        moveCameraWithMouseAction = playerInput.actions["Look"];
        endGameAction = playerInput.actions["CloseGame"];
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cam.transform.right.normalized + move.z * cam.transform.forward.normalized;
        move.y = 0;
        characterController.Move(move * 12 * Time.deltaTime);

        Vector2 mouseInput = moveCameraWithMouseAction.ReadValue<Vector2>();
        mouseX = mouseInput.x * sensitivtyX;
        mouseY = mouseInput.y * sensitivtyY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        cam.eulerAngles = targetRotation;

        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        if (endGameAction.triggered)
        {
            Application.Quit();
        }
    }
}
