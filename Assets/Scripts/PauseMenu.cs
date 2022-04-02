using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction pauseAction;
    public static bool gameIsPaused = false;
    public GameObject cam;
    public GameObject pauseMenuUI, optionsMenuUI, howToPlayMenuUI;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions["Pause"];
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        howToPlayMenuUI.SetActive(false);
        gameIsPaused = false;
    }

    void Update()
    {
        if (pauseAction.triggered && gameIsPaused == false)
        {
            Pause();
            Debug.Log("Paused");
        }
        else if (pauseAction.triggered && gameIsPaused == true)
        {
            Resume();
            Debug.Log("Resumed");
        }
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        cam.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1.0f;
        cam.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OptionsMenu()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void HowToPlayMenu()
    {
        pauseMenuUI.SetActive(false);
        howToPlayMenuUI.SetActive(true);
    }

    public void Back()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        howToPlayMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
