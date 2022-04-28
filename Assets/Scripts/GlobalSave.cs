using System;
using UnityEngine;

public class GlobalSave : MonoBehaviour
{
    public static GlobalSave Instance;
    [NonSerializedAttribute] public float timeCoreScore;
    public bool portalOneActive = true;
    public bool portalTwoActive = true;
    public bool portalThreeActive = true;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
