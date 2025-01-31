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
      if (other.gameObject.TryGetComponent<Player>(out var player))
      {
         PickUp(player);
         Destroy(this.gameObject);
      }
   }
}
