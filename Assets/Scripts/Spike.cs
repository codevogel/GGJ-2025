using UnityEngine;

public class Spike : MonoBehaviour
{
   void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Player"))
      {
         var player = collision.gameObject.GetComponent<Player>();
         player.Pop();
         Destroy(this.gameObject);
      }
   }
}
