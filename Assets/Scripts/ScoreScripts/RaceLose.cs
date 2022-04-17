using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RaceLose : MonoBehaviour
{
    public static RaceLose Instance;
    public GameObject loseCanvas;

    IEnumerator ChangeScenes()
    {
        BGMusic.audioSource.Stop();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(3);
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        loseCanvas.SetActive(false);
    }
    public void RobotWin()
    {
        loseCanvas.SetActive(true);
        PlayerController.playerSpeed = 0f;
        StartCoroutine(ChangeScenes());
    }
}
