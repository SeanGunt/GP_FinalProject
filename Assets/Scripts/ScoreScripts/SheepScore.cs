using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SheepScore : MonoBehaviour
{
    public static SheepScore instance;
    int sheepScoreValue = 0;
    public Text score;
    public GameObject winCanvas;

    IEnumerator ChangeScenes()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        score.text = sheepScoreValue + " / 3 ";
        winCanvas.SetActive(false);
    }

    public void IncreaseScore()
    {
        sheepScoreValue += 1;
        score.text = sheepScoreValue + " / 3";
        
        if( sheepScoreValue >= 3)
        {
            winCanvas.SetActive(true);
            StartCoroutine(ChangeScenes());
            PlayerController.playerSpeed = 0f;
        }
    }
}
