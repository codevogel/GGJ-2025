using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
   private GameUI gameUI;

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
      gameUI = FindAnyObjectByType<GameUI>();
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
      currentRoutine = null;
      SceneManager.LoadScene("Victory Screen");
      GameObject.FindAnyObjectByType<VictoryScreen>().SetWinnerText(winner.name);
   }

   private void InitializePlayers()
   {
      for (int i = 0; i < players.Count(); i++)
      {
         if (players[i] == null)
            return;

         players[i].transform.position = playerSpawnPoints[i].transform.position;
         players[i].transform.rotation = playerSpawnPoints[i].transform.rotation;
         //add in resetting inflation
      }
   }

   private void InitializeArena()
   {

   }

   private GameObject DetermineWinner()
   {
      GameObject winner = players[0];
      for (int i = 1; i < players.Length; i++)
      {
         if (players[i] != null && players[i].transform.localScale.magnitude > winner.transform.localScale.magnitude)
         {
            winner = players[i];
         }
      }

      return winner;
   }

   private IEnumerator RunRound()
   {
      float time = 0;
      while (time < timeInRound)
      {
         time+= Time.deltaTime;
         gameUI.UpdateTimer(Mathf.RoundToInt(timeInRound - time));
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
         gameUI.UpdateCountdown(Mathf.RoundToInt(countdownTime - time));
         yield return null;
      }

      gameUI.DisableCountdown();
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

   public void ClearPersistents()
   {
      foreach (var player in players)
      {
         if (player != null)
            Destroy(player);
      }
      Destroy(gameObject);
   }
}
