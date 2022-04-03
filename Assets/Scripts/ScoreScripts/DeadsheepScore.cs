using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadsheepScore : MonoBehaviour
{
    public static DeadsheepScore instance;
    public int deadSheepScore = 0;
    public GameObject loseCanvas;
     
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
         loseCanvas.SetActive(false);
     }

     public void IncreaseScore()
     {
         deadSheepScore += 1;
         if (deadSheepScore >= 2)
         {
             loseCanvas.SetActive(true);
             StartCoroutine(ChangeScenes());
             PlayerController.playerSpeed = 0f;
         }
     }
}
