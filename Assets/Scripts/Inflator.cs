using UnityEngine;

public class Inflator : MonoBehaviour
{
   [SerializeField]
   private GameObject sphere;

   [SerializeField]
   private float incrementalScale = 0.1f;

   public void Inflate(float amount = -1.0f)
   {
      var incrementAmount = amount < 0 ? incrementalScale : amount;
      sphere.transform.localScale += Vector3.one * incrementAmount;
   }

}
