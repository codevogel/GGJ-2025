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
   [SerializeField]
   private float _bounceForce = 15;

   // Bounce force is lower when you have more PickupCount (At at least 10 pickups, bounce force is 25% of the original value)
   public float BounceForce
   {
      get
      {
         var percentage = Mathf.Clamp01(inflator.PickupCount / 10f);
         return _bounceForce * (1 - percentage * 0.75f);
      }
   }

   private void Start()
   {
      rb = GetComponent<Rigidbody>();
      playerController = GetComponent<PlayerController>();
   }

   public void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.TryGetComponent(out Player player))
      {
         rb.AddForce((transform.position - player.transform.position).normalized * BounceForce, ForceMode.Impulse);
      }
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
