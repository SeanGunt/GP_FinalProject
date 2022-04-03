using UnityEngine;

public class CavemanDialogue : MonoBehaviour
{
    public GameObject player;
    public GameObject dialogueBox;

    void Awake()
    {
        dialogueBox.SetActive(false);
    }

    void Update()
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
}
