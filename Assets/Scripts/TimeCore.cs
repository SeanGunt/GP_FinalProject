using System;
using UnityEngine;

public class TimeCore : MonoBehaviour
{
    public GameObject winCanvas;
    float rotationSpeed = 50f;
    private Vector3 timeCoreStartPos;
    private Vector3 otherStartPos;
    float amplitude = 1.5f;

    void Awake()
    {
        timeCoreStartPos = this.transform.position;
        otherStartPos = this.transform.position;

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
        this.transform.position = new Vector3(timeCoreStartPos.x, (Mathf.Sin(timeCoreStartPos.y += amplitude * Time.deltaTime) * 0.5f) + otherStartPos.y,
            timeCoreStartPos.z);
    }
}
