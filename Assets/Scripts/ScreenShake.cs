using UnityEngine;
using Cinemachine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
   private CinemachineVirtualCamera cinemachineCam;
   private CinemachineBasicMultiChannelPerlin noise;

   [Header("Shake Settings")]
   public float shakeIntensity = 2f;
   public float shakeDuration = 0.5f;

   private void Awake()
   {
      cinemachineCam = GetComponent<CinemachineVirtualCamera>();
      if (cinemachineCam != null)
      {
         noise = cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
      }
   }

   public void ShakeCamera()
   {
      if (noise != null)
      {
         StartCoroutine(DoShake());
      }
   }

   private IEnumerator DoShake()
   {
      noise.m_AmplitudeGain = shakeIntensity; // Set shake intensity
      yield return new WaitForSeconds(shakeDuration); // Wait for duration
      noise.m_AmplitudeGain = 0f; // Reset shake
   }
}
