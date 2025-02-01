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
   }

   public void SetWinnerText(string winner)
   {
      victoryText.text = $"{winner} wins!";
   }

   public void Replay()
   {
      ScreenFaderManager.instance?.GoToSceneAsync(0);
   }

   public void GoToMainMenu()
   {
      GameManager.instance.ClearPersistents();
      ScreenFaderManager.instance?.GoToSceneAsync(0);

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
