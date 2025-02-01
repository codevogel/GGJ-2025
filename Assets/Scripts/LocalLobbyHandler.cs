using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LocalLobbyHandler : MonoBehaviour
{
    //later playercount vervangen met de list van roundmanager
    private int playerCount;
    [SerializeField] private GameObject[] playerSlots = new GameObject[4];
    [SerializeField] private GameObject[] readyTexts = new GameObject[4];
   private Dictionary<GameObject, GameObject> playerReadyTextDictionary = new Dictionary<GameObject, GameObject>();

   [SerializeField] private TMP_Text startTimer;

   [Space]
   [SerializeField] private Material[] wizardMats;
   [SerializeField] private Material[] bubbleMats;
   [SerializeField] AudioClip playerJoinAudioClip;
   [SerializeField] AudioClip playerReadyAudioClip;

    public static LocalLobbyHandler instance;
   private bool everyoneReady;
   private Coroutine readyRoutine;

   [Space]
   [SerializeField] private float timeToStart = 5f;
   private List<LobbyReadyToggler> readyTogglerList = new List<LobbyReadyToggler>();

   private void Start()
   {
      instance = this;
   }

   private void SetWizardColor(GameObject player, int playerSlotIndex)
   {
      PlayerColorer colorer = player.GetComponent<PlayerColorer>();
      colorer.ColorWizard(wizardMats[playerSlotIndex]);
      colorer.ColorBubble(bubbleMats[playerSlotIndex]);

   }

   public void AddPlayer(PlayerInput playerInput)
   {
      GameManager.instance.JoinGame(playerInput.gameObject);

      for (int i = 0; i < playerSlots.Length; i++)
      {
         if (!playerSlots[i].activeInHierarchy)
         {
            Debug.Log("setting player");
            playerSlots[i].SetActive(true);
            //pass through to lobby controller
            PlayerController _playerController = playerInput.GetComponent<PlayerController>();
            _playerController.IsAlive = false;
            playerInput.GetComponent<Rigidbody>().isKinematic = true;
            _playerController.PlayerNumber = i;
            playerInput.name = $"Player {i + 1}";
            playerInput.gameObject.transform.position = playerSlots[i].transform.position;
            playerInput.gameObject.transform.rotation = Quaternion.identity;
            readyTogglerList.Add(playerInput.gameObject.GetComponent<LobbyReadyToggler>());

            PlaySfx.instance.playOneShotSFX(playerJoinAudioClip, playerSlots[i].transform, 1f, 1f, 1f, 1f, 0f);
            playerReadyTextDictionary.Add(playerInput.gameObject, readyTexts[i]);

            //playerInput.GetComponent<LocalLobbyTag>().ReadyText = readyTexts[i];
            playerCount++;
            SetWizardColor(_playerController.wizardModel, i);
            return;
         }
      }
   }

   public void RemovePlayer(PlayerInput playerInput)
    {
        GameManager.instance.LeaveGame(playerInput.gameObject);
        playerCount--;
    }

    public void CheckReady()
    {
        if (playerCount <= 1)
        {
            //Debug.Log("Not enough players");
            return;
        }

        for (int i = 0; i < playerCount; i++)
        {
            if (!readyTexts[i].activeInHierarchy)
            {
               SetStartTimer("Ready Up To Start");
                return;
            }
        }
      everyoneReady = true;
      if (readyRoutine == null)
         readyRoutine = StartCoroutine(RunStartTimer());
    }

   private void SetStartTimer(string text)
   {
      startTimer.text = text;
   }

   public void ToggleReadyUp(GameObject playerObject)
   {
      PlaySfx.instance?.playOneShotSFX(playerReadyAudioClip, playerObject.transform, 1f, 1f, 1f, 1f, 0f);
      playerReadyTextDictionary[playerObject].SetActive(!playerReadyTextDictionary[playerObject].activeInHierarchy);
      CheckReady();
   }

   public void StartGame()
   {
      foreach (LobbyReadyToggler toggler in readyTogglerList)
      {
         Destroy(toggler);
      }
      ScreenFaderManager.instance?.GoToSceneAsync(2);
   }

   public void ReturnToMainMenu()
   {
      Destroy(GameManager.instance.gameObject);
        ScreenFaderManager.instance?.GoToSceneAsync(0); 
   }

   private IEnumerator RunStartTimer()
   {
      float timer = 0f;
      while (timer < timeToStart)
      {
         timer += Time.deltaTime;
         if (!everyoneReady)
         {
            StopCoroutine(readyRoutine);
            SetStartTimer("Ready Up To Start");
         }
         SetStartTimer($"Starting in: {Mathf.Round(timeToStart - timer)}");
         yield return null;
      }

      StartGame();
   }
}