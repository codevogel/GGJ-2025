using System.Collections.Generic;
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

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject startButton3D;

   [Space]
   [SerializeField] private Material[] wizardMats;
   [SerializeField] private Material baseWizardMat;

    public static LocalLobbyHandler instance;

    private void Start()
    {
        instance = this;

        foreach (var item in playerSlots)
        {
            item.SetActive(false);
        }
    }

   private void SetWizardColor(GameObject player, int playerSlotIndex)
   {
      player.GetComponent<PlayerColorer>().ColorWizard(wizardMats[playerSlotIndex]);
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

    public void SetStartButton()
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
                startButton.SetActive(false);
                //startButton3D.SetActive(false);
                return;
            }
        }

        startButton.SetActive(true);
        startButton3D.SetActive(true);
    }

   public void ToggleReadyUp(GameObject playerObject)
   {
      playerReadyTextDictionary[playerObject].SetActive(!playerReadyTextDictionary[playerObject].activeInHierarchy);
      SetStartButton();
   }

   public void StartGame()
   {
      SceneManager.LoadScene("Main");
   }

   public void ReturnToMainMenu()
   {
      Destroy(GameManager.instance.gameObject);
      SceneManager.LoadScene("Main Menu");
   }
}