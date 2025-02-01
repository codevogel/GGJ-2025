using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

   private GameObject[] players = new GameObject[4];
   [SerializeField] private PlayerScores[] playerScoreUIs = new PlayerScores[4];
   private Dictionary<GameObject, PlayerController> controllerDictionary = new Dictionary<GameObject, PlayerController>();
   private Dictionary<GameObject, Inflator> inflatorDictionary = new Dictionary<GameObject, Inflator>();
   private int playerCount = 0;

   [SerializeField] private float timeInRound = 60;
   [SerializeField] private float countdownTime = 3;

   [Space]
   [SerializeField] private GameObject[] playerSpawnPoints = new GameObject[4];

   private Coroutine currentRoutine;
   private GameUI gameUI;
   public string winnerName;

   public bool gameRunning;

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
            inflatorDictionary.Add(playerObject, playerObject.GetComponent<Inflator>());
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
      for (int i = 0; i < players.Length; i++)
      {
         if (players[i] != null)
         {
            playerScoreUIs[i].gameObject.SetActive(true);
         }
      }
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
      ScreenFaderManager.instance?.GoToSceneAsync(3);
      winnerName = winner.name;
   }

   private void InitializePlayers()
   {
      for (int i = 0; i < players.Length; i++)
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
      gameRunning = false;
      GameObject winner = players[0];
      int winnerCount = players[0].GetComponent<Inflator>().PickupCount;
      Debug.Log($"winnercount is {winnerCount}");
      for (int i = 1; i < players.Length; i++)
      {
         if (players[i] == null) return winner;
         int newCount = players[i].GetComponent<Inflator>().PickupCount;
         Debug.Log($"newcount = {newCount}, winnercount = {winnerCount}");
         if (newCount > winnerCount)
         {
            winnerCount = newCount;
            winner = players[i];
         }
      }

      return winner;
   }

   private IEnumerator RunRound()
   {

      FindAnyObjectByType<DynamicObstacleSpawner>().StartSpawning();

      float time = 0;
      while (time < timeInRound)
      {
         time += Time.deltaTime;
         gameUI.UpdateTimer(Mathf.RoundToInt(timeInRound - time));
         for (int i = 0; i < players.Length; i++)
         {
            if (players[i] != null)
            {
               playerScoreUIs[i].SetScore(inflatorDictionary[players[i]].PickupCount.ToString());
            }
         }
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
      gameRunning = true;
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
