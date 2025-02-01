
using System.Collections;
using UnityEngine;

public class SpikeyWall : MonoBehaviour
{
   public float extendDistance = 1.0f;
   public float extendInterval = 5.0f;
   public float extendTime = 3.0f;
   public float extendSpeed = 0.5f; // Extend in 0.5 seconds

   private Vector3 originalPosition;
   private Vector3 extendedPosition;
   private bool isExtending = false;

   void Start()
   {
      originalPosition = transform.position;
      extendedPosition = originalPosition - transform.up * extendDistance;
      StartCoroutine(ExtendCycle());
   }

   IEnumerator ExtendCycle()
   {
      while (true)
      {
         yield return new WaitForSeconds(extendInterval);
         yield return StartCoroutine(MoveObject(originalPosition, extendedPosition, extendSpeed));
         yield return new WaitForSeconds(extendTime);
         yield return StartCoroutine(MoveObject(extendedPosition, originalPosition, extendTime));
      }
   }

   IEnumerator MoveObject(Vector3 start, Vector3 end, float duration)
   {
      float elapsed = 0f;
      while (elapsed < duration)
      {
         transform.position = Vector3.Lerp(start, end, elapsed / duration);
         elapsed += Time.deltaTime;
         yield return null;
      }
      transform.position = end;
   }

   void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Player"))
      {
         collision.gameObject.GetComponent<Player>().Pop();
      }
   }
}

