using UnityEngine;
using UnityEngine.Events;

public class BubbleSpawner : MonoBehaviour
{
   public GameObject bubblePrefab; 
   public int numBubbles = 10; 
   public float spawnRadius = 5f; 
   public float HeightOffset = 2f;

   [SerializeField]
   bool testSpawn;

   public UnityEvent ExplodeFeedForward;

   public UnityEvent ExplodeEvent;

   private void Start()
   {
      ExplodeEvent.AddListener(SpawnBubbles);
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


}
