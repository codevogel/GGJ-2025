using UnityEngine;

public class PlayerColorer : MonoBehaviour
{
   [SerializeField] private SkinnedMeshRenderer[] wizardRends;
   [SerializeField] private MeshRenderer bubbleRend;

   public void ColorWizard(Material color)
   {
      foreach (var rend in wizardRends)
      {
         rend.material = color;
      }
   }

   public void ColorBubble(Material color)
   {
      bubbleRend.material = color;
   }
}
