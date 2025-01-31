using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleSpawnManager : MonoBehaviour
{
   [SerializeField]
   List<GameObject> currentCauldrons;

   [SerializeField]
   float waitTillNextExplosion = 10;

   [SerializeField]
   float waitTimeDamping = 0.9f;

   [SerializeField]
   float waitFeedForward = 2;

   [SerializeField]
   bool GameRunning = true;

   private void Start()
   {
      StartSpawning();
   }

   public void StartSpawning()
   {
      StartCoroutine(ExplodeTimer());
   }
   
   IEnumerator ExplodeTimer()
   {
      while (GameRunning)
      {
         GameObject nextToExplode = currentCauldrons[Random.Range(0, currentCauldrons.Count)];
         Cauldron bubbleSpawner = nextToExplode.GetComponent<Cauldron>();

         yield return new WaitForSeconds(waitTillNextExplosion);

         bubbleSpawner.ExplodeFeedForward.Invoke();
         yield return new WaitForSeconds(waitFeedForward);

         Explode(bubbleSpawner);

         waitTillNextExplosion *= waitTimeDamping;
      }
   }

   private void Explode(Cauldron bubbleSpawner)
   {
      bubbleSpawner.ExplodeEvent.Invoke();
   }
}
