using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
   public GameObject bubblePrefab; 
   public int bubbleCount = 10; 
   public float spawnRadius = 5f; 
   public float HeightOffset = 2f; 

   void SpawnBubbles(int numBubbles)
   {
      for (int i = 0; i < numBubbles; i++)
      {
         float angle = (i / (float)bubbleCount) * 2f * Mathf.PI;
         float x = Mathf.Cos(angle) * spawnRadius;
         float z = Mathf.Sin(angle) * spawnRadius;
         Vector3 spawnPosition = new Vector3(x, this.transform.position.y + HeightOffset, z);

         GameObject newBubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
      }
   }
}
