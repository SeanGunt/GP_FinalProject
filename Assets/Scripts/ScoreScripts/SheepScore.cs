using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SheepScore : MonoBehaviour
{
    public static SheepScore instance;
    [NonSerializedAttribute] public int sheepScoreValue = 2;
    public Text score;
    public GameObject winCanvas;
    public GameObject timeCore;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        score.text = sheepScoreValue + " / 3 ";
        winCanvas.SetActive(false);
        timeCore.SetActive(false);
    }

    public void IncreaseScore()
    {
        sheepScoreValue += 1;
        score.text = sheepScoreValue + " / 3";
        
        if( sheepScoreValue >= 3)
        {
            timeCore.SetActive(true);
        }
    }
}
