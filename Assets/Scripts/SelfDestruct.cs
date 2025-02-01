using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

   [SerializeField]
   float destroyAfter = 2;

   [SerializeField]
   GameObject particleEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      StartCoroutine(DestroyAfter());
    }

    IEnumerator DestroyAfter()
    {
      yield return new WaitForSeconds(destroyAfter);
      if(particleEffect != null)
      {
        Instantiate(particleEffect, transform.position, Quaternion.identity);
      }
      Destroy(this.gameObject);
    }
 


}
