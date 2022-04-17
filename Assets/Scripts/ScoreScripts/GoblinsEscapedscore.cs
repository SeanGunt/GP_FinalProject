using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoblinsEscapedscore : MonoBehaviour
{
    public static GoblinsEscapedscore Instance;
    [NonSerializedAttribute] public float escaped = 0;
    public Text score;
    public GameObject loseCanvas;
    IEnumerator ChangeScenes()
    {
        BGMusic.audioSource.Stop();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(2);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        escaped = 0;
        score.text = "Escaped: " + escaped.ToString();
        loseCanvas.SetActive(false);
    }

    void DestroyAllGoblins()
    {
        GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
        foreach(GameObject goblin in goblins)
        GameObject.Destroy(goblin);
        SpawnerScript.countdown = float.PositiveInfinity;
    }

    public void IncreaseScore()
    {
        escaped += 1;
        score.text = "Escaped: " + escaped.ToString();
        if (escaped >= 5)
        {
            DestroyAllGoblins();
            StartCoroutine(ChangeScenes());
            loseCanvas.SetActive(true);
            PlayerController.playerSpeed = 0f;
        }
    }
}
