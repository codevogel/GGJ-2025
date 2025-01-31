using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField]
   private Inflator inflator;
   [SerializeField]
   private GameObject GFX;

   private Rigidbody rb;
   private PlayerController playerController;

   private void Start()
   {
      rb = GetComponent<Rigidbody>();
      playerController = GetComponent<PlayerController>();
   }


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

   public void Pop()
   {
      inflator.Pop();
      Stun();
   }

   public void Stun()
   {
      playerController.enabled = false;
      rb.linearVelocity = Vector3.zero;
      rb.isKinematic = true;
      StartCoroutine(Blink(3));
      StartCoroutine(Unstun(3));
   }

   public IEnumerator Blink(float timeToBlink)
   {
      float numBlinks = timeToBlink / 0.25f;
      for (int i = 0; i < numBlinks; i++)
      {
         GFX.SetActive(!GFX.activeSelf);
         yield return new WaitForSeconds(0.25f);
      }
      GFX.SetActive(true);
   }

   public IEnumerator Unstun(float time)
   {
      yield return new WaitForSeconds(time);
      playerController.enabled = true;
      rb.isKinematic = false;
   }
}
