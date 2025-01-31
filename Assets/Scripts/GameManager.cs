using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

   private GameObject[] players = new GameObject[4];
   private Dictionary<GameObject, PlayerController> controllerDictionary= new Dictionary<GameObject, PlayerController>();
   private int playerCount = 0;

   [SerializeField] private float timeInRound = 60;
   [SerializeField] private float countdownTime = 3;

   [Space]
   [SerializeField] private GameObject[] playerSpawnPoints = new GameObject[4];

   private Coroutine currentRoutine;

   private void Start()
   {
      DontDestroyOnLoad(this);
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
            controllerDictionary.Add(playerObject, playerObject.GetComponent<PlayerController>());
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

   public void StartGame()
   {
      playerSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawns");
      InitializePlayers();
      InitializeArena();

      if (currentRoutine == null)
      {
         currentRoutine = StartCoroutine(RunCountdown());
      }
   }

   private void EndGame(GameObject winner)
   {

   }

   private void InitializePlayers()
   {
      for (int i = 0; i < players.Count(); i++)
      {
         if (players[i] == null)
            return;

         players[i].transform.position = playerSpawnPoints[i].transform.position;
         players[i].transform.rotation = playerSpawnPoints[i].transform.rotation;
      }
   }

   private void InitializeArena()
   {

   }

   private GameObject DetermineWinner()
   {
      return null;
   }

   private IEnumerator RunRound()
   {
      float time = 0;
      while (time < timeInRound)
      {
         time+= Time.deltaTime;
         yield return null;
      }

      EndGame(DetermineWinner());
   }

   private IEnumerator RunCountdown()
   {
      float time = 0;
      while (time < countdownTime)
      {
         time += Time.deltaTime;
         yield return null;
      }

      foreach (var player in players)
      {
         if (player != null)
         {
            controllerDictionary[player].IsAlive = true;
            player.GetComponent<Rigidbody>().isKinematic = false;
         }
      }
      currentRoutine = StartCoroutine(RunRound());
   }
}
