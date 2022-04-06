using UnityEngine;

public class CavemanDialogue : MonoBehaviour
{
    public GameObject player;
    public GameObject dialogueBox;
    public GameObject dialogueBoxWin;

    void Awake()
    {
        dialogueBox.SetActive(false);
        dialogueBoxWin.SetActive(false);
    }

    void Update()
    {
        if (SheepScore.instance.sheepScoreValue < 3)
        {
            float distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance < 10f)
            {
                dialogueBox.SetActive(true);
            }
            else
            {
                dialogueBox.SetActive(false);
            }
        }
        else if (SheepScore.instance.sheepScoreValue >= 3)
        {
            dialogueBox.SetActive(false);
            dialogueBoxWin.SetActive(true);
        }
    }
}
