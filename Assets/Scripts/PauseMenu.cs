using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction pauseAction;
    public static bool gameIsPaused = false;
    public GameObject cam;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions["Pause"];
    }

    void Update()
    {
        if(pauseAction.triggered && gameIsPaused == false)
        {
            Pause();
        }
        else if(pauseAction.triggered && gameIsPaused == true)
        {
            Resume();
        }
    }

    void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        cam.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1.0f;
        cam.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
