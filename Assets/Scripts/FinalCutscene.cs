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
    void Start()
    {
        if (GlobalSave.Instance.timeCoreScore >= 3)
        {
            finalCamera.SetActive(true);
            player.SetActive(false);
            timeSlowUI.SetActive(false);
            finalWinCanvas.SetActive(true);
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
