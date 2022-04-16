using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public bool isRandomRotation;

    public float xSpeed;
    public float ySpeed;
    public float zSpeed;

    void Start()
    {
        if (isRandomRotation)
        {
            xSpeed = Random.Range(-.5f,.5f);
            ySpeed = Random.Range(-.5f,.5f);
            zSpeed = Random.Range(-.5f,.5f);
        }
    }

    void FixedUpdate()
    {
        transform.localRotation *= Quaternion.Euler(xSpeed, ySpeed, zSpeed);
    }

}
