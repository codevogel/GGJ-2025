using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Cauldron : MonoBehaviour
{
   public GameObject bubblePrefab; 
   public int numBubbles = 10; 
   public float spawnRadius = 5f; 
   public float HeightOffset = 2f;

   [SerializeField]
   public int minExplodeTime;

   [SerializeField]
   public int maxExplodeTime;

   [SerializeField]
   public int waitFeedForward;
   [SerializeField] private AudioClip _explosionSound;

   [SerializeField]
   bool testSpawn;

   [SerializeField]
   GameObject explosion;


   public UnityEvent ExplodeFeedForward;

   public UnityEvent ExplodeEvent;

   [Space]
   [SerializeField] private bool doesDamage;
   [SerializeField] private float explosionRadius, explosionForce;
   [SerializeField] private LayerMask playerLayer;
   private Rigidbody rBody;

   private void Start()
   {
      rBody = GetComponent<Rigidbody>();
      ExplodeEvent.AddListener(ExplodeCauldron);
      ExplodeEvent.AddListener(SpawnBubbles);
      ExplodeEvent.AddListener(InstantiateExplosion);

      StartCoroutine(ExplodeTimer());
   }

   void SpawnBubbles()
   {
      for (int i = 0; i < numBubbles; i++)
      {
         float angle = (i / (float)numBubbles) * 2f * Mathf.PI;
         float XOffset = Mathf.Cos(angle) * spawnRadius;
         float zOffset = Mathf.Sin(angle) * spawnRadius;
         Vector3 spawnPosition = new Vector3(
            this.transform.position.x + XOffset, 
            this.transform.position.y +  HeightOffset, 
            this.transform.position.z + zOffset);

         GameObject newBubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
      }
   }

   IEnumerator ExplodeTimer()
   {
      int nextToExplode = Random.Range(minExplodeTime, maxExplodeTime);

      yield return new WaitForSeconds(nextToExplode);
      ExplodeFeedForward.Invoke();

      yield return new WaitForSeconds(waitFeedForward);
      ExplodeEvent.Invoke();
   }

   private void ExplodeCauldron()
   {
      Collider[] playerCollis = Physics.OverlapSphere(transform.position, explosionRadius, playerLayer);
      if (playerCollis.Length > 0)
      {
         foreach (Collider collider in playerCollis)
         {
            collider.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0, ForceMode.Impulse);

            if (doesDamage)
               collider.GetComponentInParent<Player>().Pop();
         }
      }
      Destroy(this.gameObject);
   }

   public void InstantiateExplosion()
   {
      Instantiate(explosion, this.transform.position, Quaternion.identity);
      PlaySfx.instance?.playOneShotSFX(_explosionSound, transform, 0.6f, 0.8f, 1f, 1f, 0f);

   }

}
