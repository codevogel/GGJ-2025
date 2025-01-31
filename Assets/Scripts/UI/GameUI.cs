using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
   [SerializeField] private TMP_Text countdownText, playTimerText;
   
   public void UpdateTimer(int time)
   {
      playTimerText.text = time.ToString();
   }

   public void UpdateCountdown(int time)
   {
      countdownText.text = time.ToString();
   }

   public void DisableCountdown()
   {
      countdownText.gameObject.SetActive(false);
   }
}
