using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ZapController : MonoBehaviour
{
   private float forceMultiplier = 125f;
   private float destroyAfterTime = 5.0f;
   public Vector3 zapBarrel { get; set;}
   Rigidbody rb;
   Camera cam;
   public GameObject zapParticles;

    void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(cam.transform.forward * forceMultiplier, ForceMode.Impulse);
        Destroy(this.gameObject, destroyAfterTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Instantiate(zapParticles, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
