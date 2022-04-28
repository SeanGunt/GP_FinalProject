using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalWinMenu : MonoBehaviour
{
    public void SwitchToMainMenu()
    {
        GlobalSave.Instance.timeCoreScore = 0;
        GlobalSave.Instance.portalOneActive = true;
        GlobalSave.Instance.portalTwoActive = true;
        GlobalSave.Instance.portalThreeActive = true;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
