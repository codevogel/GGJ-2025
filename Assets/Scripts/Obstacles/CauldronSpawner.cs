using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronSpawner : MonoBehaviour
{
   public float radius = 10f;
   public List<GameObject> obstaclePrefabs = new();

   [SerializeField]
   bool gameActive = true;

   [SerializeField]
   float spawnWaitTime = 10;

   private void Start()
   {
      StartCoroutine(Spawner());  
   }

#if UNITY_EDITOR
   public void OnDrawGizmos()
   {
      UnityEditor.Handles.color = Color.yellow;
      UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, radius);
   }
#endif


   public void SpawnCauldron()
   {
      var obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
      var randomUnitDisk = Random.insideUnitSphere;
      randomUnitDisk.y = 0;
      var position = transform.position + randomUnitDisk * radius;
      GameObject.Instantiate(obstaclePrefab, transform.position + randomUnitDisk * radius, Quaternion.identity);
   }

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         SpawnCauldron();
      }
   }

   IEnumerator Spawner()
   {
      while (gameActive)
      {
         yield return new WaitForSeconds(spawnWaitTime);
         SpawnCauldron();
         if (spawnWaitTime > 1)
         spawnWaitTime *= 0.9f;
      }
   }
}
