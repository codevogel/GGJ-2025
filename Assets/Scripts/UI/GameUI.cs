using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
   [SerializeField] private TMP_Text countdownText, playTimerText;
   
   public void UpdateTimer(string text)
   {
      playTimerText.text = text;
   }

   public void UpdateCountdown(string time)
   {
      countdownText.text = time;
   }

   public void DisableCountdown()
   {
      countdownText.gameObject.SetActive(false);
   }
}
