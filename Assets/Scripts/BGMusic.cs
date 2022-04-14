using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public static AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
