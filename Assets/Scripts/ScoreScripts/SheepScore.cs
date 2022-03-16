using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepScore : MonoBehaviour
{
    public float ScoreValue;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = " " + ScoreValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
