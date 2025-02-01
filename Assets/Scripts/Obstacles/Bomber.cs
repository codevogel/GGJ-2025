using System.Collections;
using UnityEditor;
using UnityEngine;

public class Bomber : MonoBehaviour
{
   [SerializeField] private float radius = 10, spawnChancePerSecond = 0.1f;
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
         if (Random.Range(0, 1) < spawnChancePerSecond)
         {
            Vector2 randomCircle = Random.insideUnitCircle * radius;
            GameObject go = GameObject.Instantiate(bombPrefab, new Vector3(randomCircle.x, transform.position.y, randomCircle.y), Quaternion.identity);
         }
         yield return null;
      }
   }
}
