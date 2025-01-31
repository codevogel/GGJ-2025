using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour, IPickupable
{
   public IPickupable.PickupType pickupType;

   public float DelayDuration = 3f;
   public int numBlinks = 5;

   public void PickUp(Player picker)
   {
      picker.PickUp(this);
   }

   public void OnCollisionEnter(Collision collision)
   {
      var player = collision.gameObject.GetComponentInParent<Player>();
      if (player != null)
      {
         PickUp(player);
         Destroy(this.gameObject);
      }
   }

   public void DelayPickupable()
   {
      StartCoroutine(DelayPickupableCoroutine());
   }

   public IEnumerator DelayPickupableCoroutine()
   {
      var meshRenderer = GetComponent<MeshRenderer>();
      var delayPerBlink = DelayDuration / (numBlinks * 2);

      this.gameObject.layer = LayerMask.NameToLayer("NonPickupBubble");

      for (var i = 0; i < numBlinks; i++)
      {
         meshRenderer.enabled = false;
         yield return new WaitForSeconds(delayPerBlink);
         meshRenderer.enabled = true;
         yield return new WaitForSeconds(delayPerBlink);
      }


      this.gameObject.layer = LayerMask.NameToLayer("Bubble");

   }
}
