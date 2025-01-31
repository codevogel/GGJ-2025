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
            inflator.IncrementBubbleCount();
            break;
         default:
            throw new System.NotImplementedException();
      }
   }

   public void Pop() => inflator.Pop();
}
