using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtParticle : MonoBehaviour
{
    public GameObject dirtEffect;
    public Transform feet;
    void PlayParticle()
    {
        Instantiate(dirtEffect, feet.position, this.transform.rotation );
    }
}
