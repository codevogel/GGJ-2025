using UnityEngine;

public class LaBomba : MonoBehaviour
{
   [SerializeField] private bool doesDamage = true;
   [SerializeField] private float explosionRadius = 2, explosionForce = 12;
   [SerializeField] private LayerMask playerLayer;
   private Rigidbody rBody;
   [SerializeField] private GameObject explosion;

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
      GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
      Destroy(this.gameObject);
   }
}
