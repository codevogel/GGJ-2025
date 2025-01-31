using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
   {
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
