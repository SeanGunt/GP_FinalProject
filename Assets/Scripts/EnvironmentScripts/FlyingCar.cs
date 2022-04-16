using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCar : MonoBehaviour
{
    
    public int secondsBeforeDestroy;

    public Vector3 direction;

    public float speed;

    void Start()
    {
        Destroy(gameObject, secondsBeforeDestroy);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
