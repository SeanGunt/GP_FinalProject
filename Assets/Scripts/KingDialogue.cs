using UnityEngine;

public class KingDialogue : MonoBehaviour
{
    public GameObject player;
    public GameObject normalDialogue;
    public GameObject winDialogue;

    void Awake()
    {
        winDialogue.SetActive(false);
    }
    void Update()
    {
        if (GoblinsKilled.Instance.killed < 25)
        {
            float distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance < 15f)
            {
                normalDialogue.SetActive(true);
            }
            else
            {
                normalDialogue.SetActive(false);
            }
        }
        else if (GoblinsKilled.Instance.killed >= 25)
        {
            winDialogue.SetActive(true);
            normalDialogue.SetActive(false);
        }
    }
}
