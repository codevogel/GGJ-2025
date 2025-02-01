using System.Collections;
using UnityEditor;
using UnityEngine;

public class Bomber : MonoBehaviour
{
   [SerializeField] private float radius = 10, mintime = 3f, maxtime = 6f;
   [SerializeField] private GameObject bombPrefab;

   private Coroutine warcrimeRoutine;

   private void Awake()
   {
      warcrimeRoutine = StartCoroutine(RainHell());
   }

   private IEnumerator RainHell()
   {
      while (true)
      {
         float rnd = Random.Range(mintime, maxtime);
         yield return new WaitForSeconds(rnd);

         Vector2 randomCircle = Random.insideUnitCircle * radius;
         GameObject go = GameObject.Instantiate(bombPrefab, new Vector3(randomCircle.x, transform.position.y, randomCircle.y), Quaternion.identity);
      }
   }
}
