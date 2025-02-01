using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ButtonSelectedAudioTrigger : MonoBehaviour
{
   EventSystem eventSystem;
   private GameObject selectedObject;
   [SerializeField] private AudioClip selectAudioClip;
   [SerializeField] private TMP_Text text;

   private void Awake()
   {
      
   }

   private void Awake()
   {
      
   }

   private void Start()
   {
      eventSystem = FindAnyObjectByType<EventSystem>();
      selectedObject = eventSystem.currentSelectedGameObject;


   }

   private GameObject previousSelectedObject;

   private void Update()
   {
      if (eventSystem.currentSelectedGameObject != selectedObject && eventSystem.currentSelectedGameObject == gameObject)
      {
         PlaySfx.instance.playOneShotSFX(selectAudioClip, transform, 1, 1, 1, 1, 0);

         // Reset the font style of the previously selected object
         if (previousSelectedObject != null)
         {
            var previousText = previousSelectedObject.GetComponentInChildren<TextMeshProUGUI>();
            if (previousText != null)
            {
               previousText.fontStyle = FontStyles.Normal;
            }
         }

         // Underline the currently selected object
         selectedObject = eventSystem.currentSelectedGameObject;
         var currentText = selectedObject.GetComponentInChildren<TextMeshProUGUI>();
         if (currentText != null)
         {
            currentText.fontStyle = FontStyles.Underline;
         }

         // Update the previous selected object
         previousSelectedObject = selectedObject;
      }
   }
}
