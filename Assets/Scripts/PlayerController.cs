using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xRotation = 0f;
    private float speed = 6f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocty;
    private float mouseSensitivty = 500f;
    public CharacterController controller;
    public Transform cam;
    public Transform player;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {   //character movement//
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocty, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        //camera movement//
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivty * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivty * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.Rotate(Vector3.right * xRotation);
        player.Rotate(Vector3.up * mouseX);
    }
}
