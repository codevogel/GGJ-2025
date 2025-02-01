using UnityEngine;

public class Credits : MonoBehaviour
{
   public void GoToMainMenu()
   {
      ScreenFaderManager.instance?.GoToSceneAsync(0);
   }
}
