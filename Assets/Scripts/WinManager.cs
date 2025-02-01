using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
   public Material player1Material;
   public Material player1BubbleMaterial;
   public Material player2Material;
   public Material player2BubbleMaterial;
   public Material player3Material;
   public Material player3BubbleMaterial;
   public Material player4Material;
   public Material player4BubbleMaterial;

   public GameObject losingPlayer1;
   public GameObject losingPlayer2;
   public GameObject losingPlayer3;

   public MeshRenderer bubbleRendererWinningPlayer;
   public List<SkinnedMeshRenderer> skinnedRenderersWinningPlayer;

   public MeshRenderer bubbleRendererLosingPlayer1;
   public List<SkinnedMeshRenderer> skinnedRenderersLosingPlayer1;
   public MeshRenderer bubbleRendererLosingPlayer2;
   public List<SkinnedMeshRenderer> skinnedRenderersLosingPlayer2;
   public MeshRenderer bubbleRendererLosingPlayer3;
   public List<SkinnedMeshRenderer> skinnedRenderersLosingPlayer3;

   void Start()
   {
      SetWinningPlayer(WinData.GetWinningPlayer());
      SetAmountOfPlayers(WinData.playerCount);

      StartCoroutine(LoadMainMenuAfterDelay());
   }

   private IEnumerator LoadMainMenuAfterDelay()
   {
      yield return new WaitForSeconds(18);
      LoadMainMenu();
   }

   public void LoadMainMenu()
   {
      SceneManager.LoadScene("Main Menu");
   }

   public void SetAmountOfPlayers(int amount)
   {
      switch (amount)
      {
         case 2:
            losingPlayer1.SetActive(true);
            losingPlayer2.SetActive(false);
            losingPlayer3.SetActive(false);
            break;
         case 3:
            losingPlayer1.SetActive(true);
            losingPlayer2.SetActive(true);
            losingPlayer3.SetActive(false);
            break;
         case 4:
            losingPlayer1.SetActive(true);
            losingPlayer2.SetActive(true);
            losingPlayer3.SetActive(true);
            break;
         default:
            throw new NotImplementedException();
      }
   }

   public void SetWinningPlayer(int player)
   {
      switch (player)
      {
         case 1:
            bubbleRendererWinningPlayer.material = player1BubbleMaterial;
            foreach (var skinnedRenderer in skinnedRenderersWinningPlayer)
            {
               skinnedRenderer.material = player1Material;
            }

            if (losingPlayer1.activeSelf)
            {
               bubbleRendererLosingPlayer1.material = player2BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer1)
               {
                  skinnedRenderer.material = player2Material;
               }
            }
            if (losingPlayer2.activeSelf)
            {
               bubbleRendererLosingPlayer2.material = player3BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer3)
               {
                  skinnedRenderer.material = player3Material;
               }
            }
            if (losingPlayer3.activeSelf)
            {
               bubbleRendererLosingPlayer3.material = player4BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer3)
               {
                  skinnedRenderer.material = player4Material;
               }
            }

            break;
         case 2:
            bubbleRendererWinningPlayer.material = player2BubbleMaterial;
            foreach (var skinnedRenderer in skinnedRenderersWinningPlayer)
            {
               skinnedRenderer.material = player2Material;
            }

            if (losingPlayer1.activeSelf)
            {
               bubbleRendererLosingPlayer1.material = player1BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer1)
               {
                  skinnedRenderer.material = player1Material;
               }
            }

            if (losingPlayer2.activeSelf)
            {
               bubbleRendererLosingPlayer2.material = player3BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer2)
               {
                  skinnedRenderer.material = player3Material;
               }
            }

            if (losingPlayer3.activeSelf)
            {
               bubbleRendererLosingPlayer3.material = player4BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer3)
               {
                  skinnedRenderer.material = player4Material;
               }
            }

            break;
         case 3:
            bubbleRendererWinningPlayer.material = player3BubbleMaterial;
            foreach (var skinnedRenderer in skinnedRenderersWinningPlayer)
            {
               skinnedRenderer.material = player3Material;
            }

            if (losingPlayer1.activeSelf)
            {
               bubbleRendererLosingPlayer1.material = player1BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer1)
               {
                  skinnedRenderer.material = player1Material;
               }
            }

            if (losingPlayer2.activeSelf)
            {
               bubbleRendererLosingPlayer2.material = player2BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer2)
               {
                  skinnedRenderer.material = player2Material;
               }
            }

            if (losingPlayer3.activeSelf)
            {
               bubbleRendererLosingPlayer3.material = player4BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer3)
               {
                  skinnedRenderer.material = player4Material;
               }
            }

            break;
         case 4:
            bubbleRendererWinningPlayer.material = player4BubbleMaterial;
            foreach (var skinnedRenderer in skinnedRenderersWinningPlayer)
            {
               skinnedRenderer.material = player4Material;
            }

            if (losingPlayer1.activeSelf)
            {
               bubbleRendererLosingPlayer1.material = player1BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer1)
               {
                  skinnedRenderer.material = player1Material;
               }
            }

            if (losingPlayer2.activeSelf)
            {
               bubbleRendererLosingPlayer2.material = player2BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer2)
               {
                  skinnedRenderer.material = player2Material;
               }
            }

            if (losingPlayer3.activeSelf)
            {
               bubbleRendererLosingPlayer3.material = player3BubbleMaterial;
               foreach (var skinnedRenderer in skinnedRenderersLosingPlayer3)
               {
                  skinnedRenderer.material = player3Material;
               }
            }
            break;
         default:
            throw new NotImplementedException();
      }


   }

}
