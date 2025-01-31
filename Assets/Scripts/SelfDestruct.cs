using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

   [SerializeField]
   float destroyAfter = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      StartCoroutine(DestroyAfter());
    }

    IEnumerator DestroyAfter()
    {
      yield return new WaitForSeconds(destroyAfter);
      Destroy(this.gameObject);
    }
 


}
