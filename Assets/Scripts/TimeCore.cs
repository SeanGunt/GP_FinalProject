using System;
using UnityEngine;

public class TimeCore : MonoBehaviour
{
    public GameObject winCanvas;
    float rotationSpeed = 50f;
    private Vector3 timeCoreStartPos;
    float amplitude = 1.5f;

    void Awake()
    {
        timeCoreStartPos = this.transform.position;
    }
    public void HandleTimeCoreInteraction()
    {
        winCanvas.SetActive(true);
        GlobalSave.Instance.timeCoreScore += 1;
        Destroy(this.gameObject);
    }

    void Update()
    {
        HandleMovemement();
        Debug.Log(timeCoreStartPos);
    }

    private void HandleMovemement()
    {
        HandleRotation();
        HandleBobbing();
    }

    private void HandleRotation()
    {
        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, this.transform.eulerAngles.y + Time.deltaTime * rotationSpeed, this.transform.rotation.z);
    }
    
    private void HandleBobbing()
    {
        this.transform.position = new Vector3(timeCoreStartPos.x, (Mathf.Sin(timeCoreStartPos.y += amplitude * Time.deltaTime) * 0.5f) + 3f,
            timeCoreStartPos.z);
    }
}
