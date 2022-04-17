using UnityEngine;

public class TimeCoreEnablers : MonoBehaviour
{
    public GameObject timeCore1, timeCore2, timeCore3;
    void Start()
    {
        if (GlobalSave.Instance.timeCoreScore == 0)
        {
            timeCore1.SetActive(false);
            timeCore2.SetActive(false);
            timeCore3.SetActive(false);
        }

        if(GlobalSave.Instance.timeCoreScore == 1)
        {
            timeCore1.SetActive(true);
            timeCore2.SetActive(false);
            timeCore3.SetActive(false);
        }

        if (GlobalSave.Instance.timeCoreScore == 2)
        {
            timeCore1.SetActive(true);
            timeCore2.SetActive(true);
            timeCore3.SetActive(false);
        }

        if (GlobalSave.Instance.timeCoreScore >= 3)
        {
            timeCore1.SetActive(true);
            timeCore2.SetActive(true);
            timeCore3.SetActive(true);
        }
    }
}
