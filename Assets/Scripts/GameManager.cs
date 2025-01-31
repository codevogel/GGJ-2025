using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

   private GameObject[] players = new GameObject[4];
   private int playerCount = 0;

   private void Start()
   {
      instance = this;
   }

   /// <summary>
   /// adds a player to the known player list.
   /// </summary>
   /// <param name="playerObject"> the player added to the list </param>
   public void JoinGame(GameObject playerObject)
   {
      if (players.Contains(playerObject))
      {
         Debug.LogError("Player has already joined");
         return;
      }

      for (int i = 0; i < players.Length; i++)
      {
         if (players[i] == null)
         {
            players[i] = playerObject;
            playerCount++;
            return;
         }
      }
   }

   /// <summary>
   /// removes player from the known player list.
   /// </summary>
   /// <param name="playerObject"> the player to be removed </param>
   public void LeaveGame(GameObject playerObject)
   {
      for (int i = 0; i < players.Length; i++)
      {
         if (players[i] == playerObject)
         {
            players[i] = null;
            playerCount--;
            return;
         }
      }
   }
}
