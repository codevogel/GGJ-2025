using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonSelectedAudioTrigger : MonoBehaviour
{
   EventSystem eventSystem;
   private GameObject selectedObject;
   [SerializeField] private AudioClip selectAudioClip;

   private void Awake()
   {
      
   }

   private void Start()
   {
      eventSystem = FindAnyObjectByType<EventSystem>();
      selectedObject = eventSystem.currentSelectedGameObject;
   }

   private void Update()
   {
      if (eventSystem.currentSelectedGameObject != selectedObject && eventSystem.currentSelectedGameObject == gameObject)
      {
         PlaySfx.instance.playOneShotSFX(selectAudioClip, transform, 1, 1, 1, 1, 0);
         selectedObject = eventSystem.currentSelectedGameObject;
      }
   }
}
