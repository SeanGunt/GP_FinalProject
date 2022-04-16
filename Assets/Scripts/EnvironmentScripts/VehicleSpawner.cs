using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    int randomVehicle;
    float randomVehicleInterval;

    [SerializeField]
    int randomMin, randomMax;
    
    public GameObject car1Prefab;
    public GameObject car2Prefab;
    public GameObject busPrefab;

    [SerializeField]
    float vehicleRotation;

    public int secondsBeforeDestroy;
    public Vector3 direction;
    public float speed;

    void Start()
    {
        randomVehicleInterval = Random.Range(2,4);
    }

    // Update is called once per frame
    void Update()
    {
        randomVehicleInterval -= Time.deltaTime;
        if (randomVehicleInterval <= 0)
        {
            randomVehicle = Random.Range(1,4);
            if (randomVehicle == 1)
            {
                GameObject go = Instantiate(car1Prefab, transform.position, Quaternion.Euler(0f, vehicleRotation, 0f));
                go.GetComponent<FlyingCar>().secondsBeforeDestroy = secondsBeforeDestroy;
                go.GetComponent<FlyingCar>().direction = direction;
                go.GetComponent<FlyingCar>().speed = speed;
            }
            else if (randomVehicle == 2)
            {
                GameObject go = Instantiate(car2Prefab, transform.position, Quaternion.Euler(0f, vehicleRotation, 0f));
                go.GetComponent<FlyingCar>().secondsBeforeDestroy = secondsBeforeDestroy;
                go.GetComponent<FlyingCar>().direction = direction;
                go.GetComponent<FlyingCar>().speed = speed;
            }
            else
            {
                GameObject go = Instantiate(busPrefab, transform.position, Quaternion.Euler(0f, vehicleRotation, 0f));
                go.GetComponent<FlyingCar>().secondsBeforeDestroy = secondsBeforeDestroy;
                go.GetComponent<FlyingCar>().direction = direction;
                go.GetComponent<FlyingCar>().speed = speed;
            }
            randomVehicleInterval = Random.Range(2,4);
        }
    }
}
