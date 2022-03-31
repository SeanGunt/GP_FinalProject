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

    //Score
    public SheepScore sheepScore;
    public DeadsheepScore deadsheepScore;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange;
    public bool playerInSightRange;

    private void start()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
        subdued = false;
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
            agent.speed = 0;
            sheepScore.ScoreValue += 1;
            subdued = true;
        }

        if (other.tag == "Pit")
        {
            deadsheepScore.DeathScoreValue += 1;
            Destroy(gameObject);

        }
          if (other.tag == "Zap")
        {
            Stunned = true;
            Debug.Log("Collided");
            Destroy(other.gameObject);
        }
    }
}
