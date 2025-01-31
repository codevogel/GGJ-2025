using System.Collections.Generic;
using UnityEngine;

public class Inflator : MonoBehaviour
{

   [SerializeField]
   private PickupSpawner pickupSpawner;

   [SerializeField]
   private Pickup bubblePickupPrefab;

   [SerializeField]
   private GameObject sphere;

   [SerializeField]
   private float incrementalScale = 0.1f;

   private int _pickupCount = 0;

   public int PickupCount
   {
      get => _pickupCount;
      private set
      {
         _pickupCount = value;
         AdjustInflation();
      }
   }

   public void IncrementBubbleCount()
   {
      PickupCount++;
   }

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         Pop();
      }
   }

   public void Pop()
   {
      List<Pickup> spawnedPickups = new List<Pickup>();
      for (var i = 0; i < PickupCount; i++)
      {
         spawnedPickups.Add(pickupSpawner.SpawnPickup(Vector3.up));
      }
      foreach (var pickup in spawnedPickups)
      {
         pickup.GetComponent<Rigidbody>().AddExplosionForce(200, sphere.transform.position - sphere.transform.localScale / 2, 10);
      }
      PickupCount = 0;
   }

   private void AdjustInflation()
   {
      sphere.transform.localScale = Vector3.one * (_pickupCount * incrementalScale + 2);
      var scaleMagnitude = sphere.transform.localScale.x;
      sphere.transform.localPosition = new Vector3(0, (scaleMagnitude - 2) / 2, 0);
   }

}
