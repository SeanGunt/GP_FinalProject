using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuModels, menuCanvas, howToPlayCanvas, optionsCanvas, backCanvas;

    [SerializeField]
    private Button startButton, howToPlayButton, optionsButton, backButton, exitButton;
    AudioSource audioSource;
    public AudioClip buttonclick;

    // Called on object Awake in Scene
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuModels.gameObject.SetActive(true);
        menuCanvas.gameObject.SetActive(true);
        howToPlayCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(false);
        backCanvas.gameObject.SetActive(false);

        startButton.onClick.AddListener(StartClick);
        howToPlayButton.onClick.AddListener(HowToPlayClick);
        optionsButton.onClick.AddListener(OptionsClick);
        backButton.onClick.AddListener(BackClick);
        exitButton.onClick.AddListener(ExitClick);
    }

    // Listener for startButton
    private void StartClick()
    {
        audioSource.PlayOneShot(buttonclick);
        SceneManager.LoadScene(1);
    }

    // Listener for howToPlayButton
    private void HowToPlayClick()
    {
        audioSource.PlayOneShot(buttonclick);
        menuModels.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(false);
        howToPlayCanvas.gameObject.SetActive(true);
        optionsCanvas.gameObject.SetActive(false);
        backCanvas.gameObject.SetActive(true);
    }

    // Listener for optionsButton
    private void OptionsClick()
    {
        audioSource.PlayOneShot(buttonclick);
        menuModels.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(false);
        howToPlayCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(true);
        backCanvas.gameObject.SetActive(true);
    }

    // Listener for backButton
    private void BackClick()
    {
        audioSource.PlayOneShot(buttonclick);
        menuModels.gameObject.SetActive(true);
        menuCanvas.gameObject.SetActive(true);
        howToPlayCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(false);
        backCanvas.gameObject.SetActive(false);
    }

    // Listener for exitButton
    private void ExitClick()
    {
        audioSource.PlayOneShot(buttonclick);
        Application.Quit();
    }
}
