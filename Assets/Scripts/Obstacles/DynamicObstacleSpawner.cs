using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacleSpawner : MonoBehaviour
{
   public float radius = 10f;
   public List<DynamicObstacle> obstaclePrefabs = new();

   public void OnDrawGizmos()
   {
      UnityEditor.Handles.color = Color.yellow;
      UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, radius);
   }

   public void SpawnRandomObstacle()
   {
      var obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
      var randomUnitDisk = Random.insideUnitSphere;
      randomUnitDisk.y = 0;
      var position = transform.position + randomUnitDisk * radius;
      GameObject.Instantiate(obstaclePrefab, transform.position + randomUnitDisk * radius, Quaternion.identity);
   }

   void Update()
   {
      //TODO: koppelen
      if (Input.GetKeyDown(KeyCode.Space))
      {
         SpawnRandomObstacle();
      }
   }

}
