using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
   public float radius = 10f;
   public List<Pickup> pickupPrefabs = new();

   public void OnDrawGizmos()
   {
      UnityEditor.Handles.color = Color.yellow;
      UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, radius);
   }

   public Pickup SpawnPickup(Vector3 offset)
   {
      var prefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Count)];
      var randomUnitDisk = Random.insideUnitSphere;
      randomUnitDisk.y = 0;
      var position = transform.position + randomUnitDisk * radius;
      var go = GameObject.Instantiate(prefab, transform.position + randomUnitDisk * radius, Quaternion.identity);
      go.transform.position += offset;
      Debug.Log(go.transform.position);
      var pickup = go.GetComponent<Pickup>();
      pickup.DelayPickupable();
      return pickup;
   }

}
