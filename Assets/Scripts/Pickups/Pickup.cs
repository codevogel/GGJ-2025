using UnityEngine;

public class Pickup : MonoBehaviour, IPickupable
{
   public IPickupable.PickupType pickupType;

   public void PickUp(Player picker)
   {
      picker.PickUp(this);
   }

   public void OnTriggerEnter(Collider other)
   {
      var player = other.gameObject.GetComponentInParent<Player>();
      if (player != null)
      {
         PickUp(player);
         Destroy(this.gameObject);
      }
   }
}
