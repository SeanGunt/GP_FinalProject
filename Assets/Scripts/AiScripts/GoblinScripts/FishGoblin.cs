using UnityEngine;
using UnityEngine.AI;


public class FishGoblin : MonoBehaviour
{
    public Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;
    AudioSource audioSource;
    public AudioClip grunt;
    float randomGruntTimer;
    public GameObject goblinDeathParticles;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        randomGruntTimer = Random.Range(3,15);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        randomGruntTimer -= Time.deltaTime;
        if (randomGruntTimer <= 0)
        {
            audioSource.PlayOneShot(grunt);
            randomGruntTimer = Random.Range(3,15);
        }
        navMeshAgent.destination = movePositionTransform.position;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoblinEnd")
        {
            GoblinsEscapedscore.Instance.IncreaseScore();
            Destroy(gameObject);   
        }

        if (other.tag == "Zap")
        {
            Instantiate(goblinDeathParticles, this.transform.position + this.transform.up, this.transform.rotation);
            GoblinsKilled.Instance.IncreaseScore();
            Destroy(gameObject);
        }
    }
}