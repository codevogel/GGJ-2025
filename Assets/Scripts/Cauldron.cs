using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Cauldron : MonoBehaviour
{
   public GameObject bubblePrefab; 
   public int numBubbles = 10; 
   public float spawnRadius = 5f; 
   public float HeightOffset = 2f;

   [SerializeField]
   public int minExplodeTime;

   [SerializeField]
   public int maxExplodeTime;

   [SerializeField]
   public int waitFeedForward;

   [SerializeField]
   bool testSpawn;

   [SerializeField]
   GameObject explosion;

   public UnityEvent ExplodeFeedForward;

   public UnityEvent ExplodeEvent;

   private void Start()
   {
      ExplodeEvent.AddListener(SpawnBubbles);
      ExplodeEvent.AddListener(ExplodeCauldron);
      ExplodeEvent.AddListener(InstantiateExplosion);

      StartCoroutine(ExplodeTimer());
   }

   void SpawnBubbles()
   {
      for (int i = 0; i < numBubbles; i++)
      {
         float angle = (i / (float)numBubbles) * 2f * Mathf.PI;
         float XOffset = Mathf.Cos(angle) * spawnRadius;
         float zOffset = Mathf.Sin(angle) * spawnRadius;
         Vector3 spawnPosition = new Vector3(
            this.transform.position.x + XOffset, 
            this.transform.position.y +  HeightOffset, 
            this.transform.position.z + zOffset);

         GameObject newBubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
      }
   }

   IEnumerator ExplodeTimer()
   {
      int nextToExplode = Random.Range(minExplodeTime, maxExplodeTime);

      yield return new WaitForSeconds(nextToExplode);
      ExplodeFeedForward.Invoke();

      yield return new WaitForSeconds(waitFeedForward);
      ExplodeEvent.Invoke();
   }

   private void ExplodeCauldron()
   {
      Destroy(this.gameObject);
   }

   public void InstantiateExplosion()
   {
      Instantiate(explosion, this.transform.position, Quaternion.identity);
   }

}
