using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuCanvas, howToPlayCanvas, optionsCanvas, backCanvas;

    [SerializeField]
    private Button startButton, howToPlayButton, optionsButton, backButton, exitButton;

    // Called on object Awake in Scene
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuCanvas.gameObject.SetActive(true);
        howToPlayCanvas.gameObject.SetActive(false);
        //optionsCanvas.gameObject.SetActive(false);
        backCanvas.gameObject.SetActive(false);

        startButton.onClick.AddListener(StartClick);
        howToPlayButton.onClick.AddListener(HowToPlayClick);
        //optionsButton.onClick.AddListener(OptionsClick);
        backButton.onClick.AddListener(BackClick);
        exitButton.onClick.AddListener(ExitClick);
    }

    // Listener for startButton
    private void StartClick()
    {
        SceneManager.LoadScene("GamePlay");
    }

    // Listener for howToPlayButton
    private void HowToPlayClick()
    {
        menuCanvas.gameObject.SetActive(false);
        howToPlayCanvas.gameObject.SetActive(true);
        optionsCanvas.gameObject.SetActive(false);
        backCanvas.gameObject.SetActive(true);
    }

    // Listener for optionsButton
    //private void OptionsClick()
    //{
        //menuCanvas.gameObject.SetActive(false);
        //howToPlayCanvas.gameObject.SetActive(false);
        //optionsCanvas.gameObject.SetActive(true);
        //backCanvas.gameObject.SetActive(true);
    //}

    // Listener for backButton
    private void BackClick()
    {
        menuCanvas.gameObject.SetActive(true);
        howToPlayCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(false);
        backCanvas.gameObject.SetActive(false);
    }

    // Listener for exitButton
    private void ExitClick()
    {
        Application.Quit();
    }
}