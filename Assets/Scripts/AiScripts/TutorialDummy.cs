using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDummy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zap")
        {
            Destroy(gameObject);
        }
    }
}
