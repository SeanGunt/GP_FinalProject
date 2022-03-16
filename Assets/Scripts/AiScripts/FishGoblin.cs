using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishGoblin : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;


    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }




}
