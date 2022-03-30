using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishGoblin : MonoBehaviour
{
    


    public Transform movePositionTransform;

    private NavMeshAgent navMeshAgent;

    public GoblinsEscapedscore goblinsEscapedscore;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
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
            Destroy(gameObject);
        }





    }




}
