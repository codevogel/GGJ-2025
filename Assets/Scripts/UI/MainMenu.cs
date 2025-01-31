using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
   [SerializeField] private AudioClip startSFX;
    public void StartGame()
   {
      StartCoroutine(IEstartGame());
   }

   IEnumerator IEstartGame()
   {
      PlaySfx.instance.playOneShotSFX(startSFX, transform, 1f, 1f, 1f, 1f, 0f);
      yield return new WaitForSeconds(startSFX.length);
      SceneManager.LoadScene("Lobby");
   }

   public void QuitGame()
   {
#if UNITY_EDITOR
      EditorApplication.isPlaying = false;
#endif
      Application.Quit();
   }

   public void ShowControls()
   {

   }

   public void HideControls()
   {

   }
}
