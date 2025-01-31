using Cinemachine;
using UnityEngine;

public class TargetGroupManager : MonoBehaviour
{
   [SerializeField]
   CinemachineTargetGroup targetGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      GameObject[] players =  GameObject.FindGameObjectsWithTag("Player");
      foreach (var player in players)
      {
         targetGroup.AddMember(player.transform, 1, 5);
      }
    }
}
