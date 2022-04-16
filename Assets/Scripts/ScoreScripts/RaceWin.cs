using UnityEngine;

public class RaceWin : MonoBehaviour
{
    public static RaceWin Instance;
    public GameObject winCanvas;
    public GameObject robotLoseDialogue;
    public BoxCollider robotWinCollider;
    public BoxCollider playerWinCollider;
    public GameObject timeCore;
    public RaceRobot raceRobot;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        winCanvas.SetActive(false);
        timeCore.SetActive(false);
        robotWinCollider.enabled = true;
        playerWinCollider.enabled = true;
        robotLoseDialogue.SetActive(false);
    }

    public void PlayerWin()
    {
        raceRobot.agent.enabled = false;
        robotWinCollider.enabled = false;
        playerWinCollider.enabled = false;
        timeCore.SetActive(true);
        robotLoseDialogue.SetActive(true);
    }
}
