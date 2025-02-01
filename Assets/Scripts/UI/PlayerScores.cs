using TMPro;
using UnityEngine;

public class PlayerScores : MonoBehaviour
{
   [SerializeField] private TMP_Text scoreText;

   public void SetScore(string score)
   {
      scoreText.text = score;
   }
}
