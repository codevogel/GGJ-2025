using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyReadyToggler : MonoBehaviour
{
   private void TryToggleReady(InputAction.CallbackContext context)
   {
      if (context.performed)
      {
         LocalLobbyHandler.instance?.ToggleReadyUp(gameObject);
      }
   }
}