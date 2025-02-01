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

   [SerializeField]
   private float popForce = 10;
   [SerializeField]
   private float popForceVerticalMin = 5;
   [SerializeField]
   private float popForceVerticalMax = 15;

   [SerializeField]
   private PlayerSounds playerSounds;

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

   public void Pop()
   {
      playerSounds.onDeath();
      List<Pickup> spawnedPickups = new List<Pickup>();
      for (var i = 0; i < Mathf.FloorToInt(PickupCount / 2); i++)
      {
         spawnedPickups.Add(pickupSpawner.SpawnPickup(Vector3.up));
      }
      foreach (var pickup in spawnedPickups)
      {
         var randomPoint = Random.insideUnitSphere;
         randomPoint.y = 0;
         pickup.GetComponent<Rigidbody>().AddExplosionForce(popForce, pickup.transform.position + randomPoint, 10);
         pickup.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(popForceVerticalMin, popForceVerticalMax), ForceMode.Impulse);
      }
      PickupCount = 0;
   }

   //void Update()
   //{
   //   if (Input.GetKeyDown(KeyCode.Space))
   //   {
   //      Pop();
   //   }
   //}

   private void AdjustInflation()
   {
      sphere.transform.localScale = Vector3.one * (_pickupCount * incrementalScale + 2);
      var scaleMagnitude = sphere.transform.localScale.x;
      sphere.transform.localPosition = new Vector3(0, (scaleMagnitude - 2) / 2, 0);
   }

}
