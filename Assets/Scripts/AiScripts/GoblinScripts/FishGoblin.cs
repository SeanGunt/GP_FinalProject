using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishGoblin : MonoBehaviour
{
    public Transform movePositionTransform;

    private NavMeshAgent navMeshAgent;

    AudioSource audioSource;
    public AudioClip grunt;
    float randomGruntTimer;

    //Score Script refrances
    public GoblinsEscapedscore goblinsEscapedscore;
    public GoblinsKilled goblinsKilled;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        randomGruntTimer = Random.Range(7,25);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        randomGruntTimer -= Time.deltaTime;
        if (randomGruntTimer <= 0)
        {
            audioSource.PlayOneShot(grunt);
            randomGruntTimer = Random.Range(7,25);
        }
        navMeshAgent.destination = movePositionTransform.position;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoblinEnd")
        {
            goblinsEscapedscore.EscapedGoblins += 1;
            Destroy(gameObject);   
        }

        if (other.tag == "Zap")
        {
            goblinsKilled.GoblinsKilledNum += 1;
            Destroy(gameObject);
        }
    }
}