using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
  AudioSource audioSource;
    public AudioClip monitor;
    float randomMonitorTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        randomMonitorTimer = Random.Range(7,25);
    }

    void Update()
    {
        randomMonitorTimer -= Time.deltaTime;
        if (randomMonitorTimer <= 0)
        {
            audioSource.PlayOneShot(monitor);
            randomMonitorTimer = Random.Range(7,25);
        }
    }
}
