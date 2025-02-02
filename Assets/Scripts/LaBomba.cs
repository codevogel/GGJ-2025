using UnityEngine;

public class LaBomba : MonoBehaviour
{
   [SerializeField] private bool doesDamage = true;
   [SerializeField] private float explosionRadius = 2, explosionForce = 12;
   [SerializeField] private LayerMask playerLayer;
   private Rigidbody rBody;
   [SerializeField] private GameObject explosion;
   [SerializeField] private AudioClip explosionSFX;

   private void OnCollisionEnter(Collision collision)
   {
      ExplodeCauldron();
   }

   private void ExplodeCauldron()
   {
      Collider[] playerCollis = Physics.OverlapSphere(transform.position, explosionRadius, playerLayer);
      if (playerCollis.Length > 0)
      {
         foreach (Collider collider in playerCollis)
         {
            if (doesDamage)
               collider.GetComponentInParent<Player>().Pop();

            collider.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0, ForceMode.Impulse);
         }
      }
      PlaySfx.instance?.playOneShotSFX(explosionSFX, transform, 0.5f, 0.6f, 0.5f, 0.6f, 0f);
      GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
      Destroy(this.gameObject);
   }
}
