using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinsEscapedscore : MonoBehaviour
{
    public float EscapedGoblins = 0;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = " " + EscapedGoblins.ToString();
    }


}
