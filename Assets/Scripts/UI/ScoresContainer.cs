using UnityEngine;

public class ScoresContainer : MonoBehaviour
{
   [SerializeField] PlayerScores[] scores = new PlayerScores[4];

   public PlayerScores[] GetScores()
   {
      return scores;
   }
}
