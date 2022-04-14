using UnityEngine;

public class KingDialogue : MonoBehaviour
{
    public GameObject player;
    public GameObject dialogueBox;
    void Awake()
    {
        
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
