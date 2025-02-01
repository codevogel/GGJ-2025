using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyReadyToggler : MonoBehaviour
{
   public void TryToggleReady()
   {
      Debug.Log("trying to ready");
      LocalLobbyHandler.instance?.ToggleReadyUp(gameObject);
   }
}