using UnityEngine;

public class PitTrigger : MonoBehaviour
{
    public GameObject innerTrigger;
    public GameObject innerTrigger2;
    public GameObject innerTrigger3;


    void OnTriggerEnter(Collider other)
    {


        if (other.tag == "CenterPit")
        {
            innerTrigger.SetActive(true);
        }

        if (other.tag == "OutterPit")
        {
            innerTrigger.SetActive(false);
        }

        if (other.tag == "CenterPit2")
        {
            innerTrigger2.SetActive(true);
        }

        if (other.tag == "OutterPit2")
        {
            innerTrigger2.SetActive(false);
        }

        if (other.tag == "CenterPit3")
        {
            innerTrigger3.SetActive(true);
        }

        if (other.tag == "OutterPit3")
        {
            innerTrigger3.SetActive(false);
        }
    }
}
