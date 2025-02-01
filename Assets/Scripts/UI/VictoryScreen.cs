using System.Collections;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
   public TMP_Text countdownText;
   public TMP_Text victoryText;

   [Space]
   public float countdownTime = 5;
   private Coroutine timerRoutine;

   [Space]
   public GameObject winningPlayer;

   private void Start()
   {
      if (timerRoutine == null)
         timerRoutine = StartCoroutine(RunCountdown());
      SetWinnerText(GameManager.instance.winnerName);
   }

   public void SetWinnerText(string winner)
   {
      Debug.Log("naming winner");
      victoryText.text = $"{winner} wins!";
   }

   public void Replay()
   {
      SceneManager.LoadScene("Main");
   }

   public void GoToMainMenu()
   {
      GameManager.instance.ClearPersistents();
      SceneManager.LoadScene("Main Menu");
   }

   public IEnumerator RunCountdown()
   {
      float timer = 0;
      while (timer < countdownTime)
      {
         timer += Time.deltaTime;
         countdownText.text = Mathf.Round(countdownTime - timer).ToString();
         yield return null;
      }
      GoToMainMenu();
   }
}
