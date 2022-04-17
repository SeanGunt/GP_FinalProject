using System;
using UnityEngine;
using UnityEngine.UI;

public class GoblinsKilled : MonoBehaviour
{
    public static GoblinsKilled Instance;
    [NonSerializedAttribute] public float killed;
    public Text score;
    public GameObject timeCore;

    void Awake()
    {
        if ( Instance == null)
        {
            Instance = this;
        }

        score.text = "Killed: " + killed.ToString();
        timeCore.SetActive(false);
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
        killed += 1;
        score.text = "Killed: " + killed.ToString();
        if (killed >= 30)
        {
            DestroyAllGoblins();
            timeCore.SetActive(true);
        }
    }
}
