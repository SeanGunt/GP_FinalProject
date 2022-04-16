using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RaceRobot : MonoBehaviour
{
    public GameObject robotStartDialogue;
    public float dialogueTimer = 10f;
    public Text startTimerText;
    public NavMeshAgent agent;
    public Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;
    // Audio
    AudioSource audioSource;
    public AudioClip robot;
    float randomRobotTimer;
    //Slow
    float slowTime = 3f;
    bool Slowed;
    //Animation
    public Animator anim;

    private void Awake()
    {
        agent.speed = 22.5f;
        agent.enabled = false;
        audioSource = GetComponent<AudioSource>();
        randomRobotTimer = Random.Range(5,20);
        navMeshAgent = GetComponent<NavMeshAgent>();
        robotStartDialogue.SetActive(true);
        startTimerText.enabled = true;
        startTimerText.text = "Time Until Race: " + dialogueTimer.ToString("n1");
    }

    void Update()
    {
        dialogueTimer -= Time.deltaTime;
        startTimerText.text = "Time Until Race: " + dialogueTimer.ToString("n1");
        {
            if (dialogueTimer <= 0)
            {
                agent.enabled = true;
                dialogueTimer = float.PositiveInfinity;
                startTimerText.enabled = false;
                robotStartDialogue.SetActive(false);
            }
        }
        randomRobotTimer -= Time.deltaTime;
        if (randomRobotTimer <= 0)
        {
            audioSource.PlayOneShot(robot);
            randomRobotTimer = Random.Range(5,20);
        }
        if (agent.enabled == true) 
        {
            navMeshAgent.destination = movePositionTransform.position;
        }
        agent = GetComponent<NavMeshAgent>();

        if (Slowed)
        {
            agent.speed = 0f;
            slowTime -= Time.deltaTime;
            if (slowTime <= 0)
            {
                agent.speed = 22.5f;
                slowTime = 3f;
                Slowed = false;
            }
        }

        anim.SetFloat("Speed", agent.speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RobotEnd")
        {
            agent.speed = 0;
            RaceLose.Instance.RobotWin();
        }

        if (other.tag == "Zap")
        {
            Slowed = true;
            Debug.Log("Collided");
            Destroy(other.gameObject);
        }
    }
}