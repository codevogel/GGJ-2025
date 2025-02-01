using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacleSpawner : MonoBehaviour
{
   public float radius = 10f;
   public List<DynamicObstacle> obstaclePrefabs = new();

   public float secondsBeforeSpawnMin = 3;
   public float secondsBeforeSpawnMax = 7;

   private Coroutine spawnObstaclesCoroutine;

#if UNITY_EDITOR
   public void OnDrawGizmos()
   {
      UnityEditor.Handles.color = Color.yellow;
      UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, radius);
   }
#endif

   public void SpawnRandomObstacle()
   {
      var obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
      var randomUnitDisk = Random.insideUnitSphere;
      randomUnitDisk.y = 0;
      var position = transform.position + randomUnitDisk * radius;
      GameObject.Instantiate(obstaclePrefab, transform.position + randomUnitDisk * radius, Quaternion.identity);
   }

   public void StartSpawning()
   {
      spawnObstaclesCoroutine = StartCoroutine(SpawnObstacles());
   }

   public void StopSpawning()
   {
      StopCoroutine(spawnObstaclesCoroutine);
   }

   public IEnumerator SpawnObstacles()
   {
      while (true)
      {
         SpawnRandomObstacle();
         yield return new WaitForSeconds(Random.Range(secondsBeforeSpawnMin, secondsBeforeSpawnMax));
      }
   }
}
