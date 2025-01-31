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
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject startButton3D;

    [Space]
    [SerializeField] private Material player2Material;
    [SerializeField] private Material player3Material;
    [SerializeField] private Material player4Material;

    //[Space]
    //[SerializeField] private Material ballMaterialRed;
    //[SerializeField] private Material ballMaterialGreen;
    //[SerializeField] private Material ballMaterialBlue;
    //[SerializeField] private Material ballMaterialYellow;

    //private float greenSaturationPercent = 70f;
    //private float blueSaturationPercent = 60f;
    //private float yellowSaturationPercent = 90f;

    public static LocalLobbyHandler instance;

    private void Start()
    {
        instance = this;

        foreach (var item in playerSlots)
        {
            item.SetActive(false);
        }
    }

   //private void SetBallColorAndHamsterMaterial(GameObject player, int playerSlotIndex)
   //{
   //   AnimatedHamster _animatedHamster = player.GetComponentInChildren<AnimatedHamster>();

   //   player.GetComponent<MeshRenderer>().material = ballMaterialRed;

   //   if (playerSlotIndex == 1)
   //   {
   //      //player.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
   //      player.GetComponent<MeshRenderer>().material = ballMaterialGreen;
   //      _animatedHamster.SetMaterial(player2Material);
   //      //player.GetComponent<MeshRenderer>().material.SetFloat("_SaturationPercent", greenSaturationPercent);
   //   }
   //   else if (playerSlotIndex == 2)
   //   {
   //      player.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
   //      player.GetComponent<MeshRenderer>().material = ballMaterialBlue;
   //      _animatedHamster.SetMaterial(player3Material);
   //      //player.GetComponent<MeshRenderer>().material.SetFloat("_SaturationPercent", blueSaturationPercent);
   //   }
   //   else if (playerSlotIndex == 3)
   //   {
   //      player.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);
   //      player.GetComponent<MeshRenderer>().material = ballMaterialYellow;
   //      _animatedHamster.SetMaterial(player4Material);
   //      //player.GetComponent<MeshRenderer>().material.SetFloat("_SaturationPercent", yellowSaturationPercent);
   //   }
   //}

   public void AddPlayer(PlayerInput playerInput)
   {
      GameManager.instance.JoinGame(playerInput.gameObject);

      for (int i = 0; i < playerSlots.Length; i++)
      {
         if (!playerSlots[i].activeInHierarchy)
         {
            playerSlots[i].SetActive(true);
            //pass through to lobby controller
            PlayerController _playerController = playerInput.GetComponent<PlayerController>();
            _playerController.IsAlive = false;
            playerInput.GetComponent<Rigidbody>().isKinematic = true;
            _playerController.PlayerNumber = i;
            playerInput.gameObject.transform.position = playerSlots[i].transform.position;
            playerInput.gameObject.transform.rotation = Quaternion.identity;
            //_playerController.AnimatedHamster.transform.forward = playerSlots[i].transform.forward;
            //playerInput.GetComponent<LocalLobbyTag>().ReadyText = readyTexts[i];
            playerCount++;
            //SetBallColorAndHamsterMaterial(playerInput.gameObject, i);
            return;
         }
      }
   }

   public void RemovePlayer(GameObject playerObject)
    {
        GameManager.instance.LeaveGame(playerObject);
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
                startButton3D.SetActive(false);
                return;
            }
        }

        startButton.SetActive(true);
        startButton3D.SetActive(true);
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