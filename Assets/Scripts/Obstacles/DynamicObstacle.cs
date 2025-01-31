using UnityEngine;

public class DynamicObstacle : MonoBehaviour
{
   [SerializeField]
   private float rotationSpeed = 2;

   private Rigidbody rb;

   void Start()
   {
      rb = GetComponent<Rigidbody>();
      rb.angularVelocity = Random.insideUnitSphere * rotationSpeed;
   }

   void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Floor"))
      {
         Destroy(this.gameObject);
      }
      if (collision.gameObject.CompareTag("Player"))
      {
         var player = collision.gameObject.GetComponent<Player>();
         player.Pop();
         Destroy(this.gameObject);
      }
   }

}
