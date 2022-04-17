using System.Diagnostics.Tracing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour
{
    public GameObject finalCamera;
    public GameObject player;
    public GameObject timeSlowUI;
    public GameObject finalWinCanvas;
    public GameObject pauseMenu;
    AudioSource audioSource;
    public AudioClip finalSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (GlobalSave.Instance.timeCoreScore >= 3)
        {
            finalCamera.SetActive(true);
            player.SetActive(false);
            timeSlowUI.SetActive(false);
            finalWinCanvas.SetActive(true);
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            BGMusic.audioSource.Stop();
            audioSource.clip = finalSound;
            audioSource.Play();
        }
        else
        {
            pauseMenu.SetActive(true);
            finalCamera.SetActive(false);
            player.SetActive(true);
            timeSlowUI.SetActive(true);
            finalWinCanvas.SetActive(false);
        }
    }
}
