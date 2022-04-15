using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinsKilled : MonoBehaviour
{
    public float GoblinsKilledNum;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = " " + GoblinsKilledNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
