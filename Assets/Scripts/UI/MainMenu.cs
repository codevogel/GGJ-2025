using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
   [SerializeField] private AudioClip startSFX, navigateSFX;
    public void StartGame()
   {
      PlaySfx.instance?.playOneShotSFX(startSFX, transform, 1f, 1f, 1f, 1f, 0f);
      ScreenFaderManager.instance?.GoToSceneAsync(1);

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
