using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepRoam : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    private bool subdued;
    //Stun
    float stunTime = 2.5f;
    bool Stunned;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //States
    public float sightRange;
    public bool playerInSightRange;
    //Audio
    AudioSource audioSource;
    public AudioClip bleat;
    float randomBleatTimer;
    //Animation
    public Animator anim;
    //Score

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        subdued = false;
        randomBleatTimer = Random.Range(7,25);
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange) Patroling();
        if (playerInSightRange) ChasePlayer();

        if (Stunned && !subdued)
        {
            agent.speed = 0;
            stunTime -= Time.deltaTime;
            if (stunTime <= 0)
            {
                agent.speed = 10f;
                stunTime = 2.5f;
                Stunned = false;
            }
        }

        randomBleatTimer -= Time.deltaTime;
        if (randomBleatTimer <= 0)
        {
            audioSource.PlayOneShot(bleat);
            randomBleatTimer = Random.Range(7,25);
        }

        anim.SetFloat("Speed", agent.speed);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1.5f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        Vector3 dirToPlayer = transform.position - player.transform.position;

        Vector3 newPos = transform.position + dirToPlayer;

        agent.SetDestination(newPos);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pen")
        {
            SheepScore.instance.IncreaseScore();
            agent.speed = 0;
            subdued = true;
        }

        if (other.tag == "Pit")
        {
            Destroy(gameObject);
            DeadsheepScore.instance.IncreaseScore();
        }

          if (other.tag == "Zap")
        {
            Stunned = true;
            Debug.Log("Collided");
            Destroy(other.gameObject);
        }
    }
}
