using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadsheepScore : MonoBehaviour
{
    public float DeathScoreValue;
    public Text Deathscore;

    // Start is called before the first frame update
    void Start()
    {
        Deathscore.text = " " + DeathScoreValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
