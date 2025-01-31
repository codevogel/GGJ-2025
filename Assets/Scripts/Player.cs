using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField]
   private Inflator inflator;

   public void PickUp(Pickup pickup)
   {
      Debug.Log("Picked up " + pickup + $" with type {pickup.pickupType}");

      switch (pickup.pickupType)
      {
         case IPickupable.PickupType.Bubble:
            inflator.Inflate();
            break;
         default:
            throw new System.NotImplementedException();
      }
   }
}
