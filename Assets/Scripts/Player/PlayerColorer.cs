using UnityEngine;

public class PlayerColorer : MonoBehaviour
{
   [SerializeField] private SkinnedMeshRenderer[] rends;

   public void ColorWizard(Material color)
   {
      foreach (var rend in rends)
      {
         rend.material = color;
      }
   }
}
