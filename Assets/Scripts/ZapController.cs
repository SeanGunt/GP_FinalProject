using UnityEngine;

public class ZapController : MonoBehaviour
{
   private float speed = 75f;
   public Vector3 target { get; set;}

    void Update()
    {
        // causes bullet to travel towards the target, the target gets set in the player controller //
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
