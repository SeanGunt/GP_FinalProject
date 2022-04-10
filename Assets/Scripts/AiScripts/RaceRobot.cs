using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaceRobot : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform movePositionTransform;

    private NavMeshAgent navMeshAgent;

    // Audio
    AudioSource audioSource;

    public AudioClip robot;

    float randomRobotTimer;

    //Slow
    float slowTime = 2.5f;
    bool Slowed;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        randomRobotTimer = Random.Range(7,25);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        randomRobotTimer -= Time.deltaTime;
        if (randomRobotTimer <= 0)
        {
            audioSource.PlayOneShot(robot);
            randomRobotTimer = Random.Range(7,25);
        }
        navMeshAgent.destination = movePositionTransform.position;
        agent = GetComponent<NavMeshAgent>();

        if (Slowed)
        {
            agent.speed = 5;
            slowTime -= Time.deltaTime;
            if (slowTime <= 0)
            {
                agent.speed = 10f;
                slowTime = 2.5f;
                Slowed = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RobotEnd")
        {
            agent.speed = 0;
            
        }

        if (other.tag == "Zap")
        {
            Slowed = true;
            Debug.Log("Collided");
            Destroy(other.gameObject);
        }
    }
}