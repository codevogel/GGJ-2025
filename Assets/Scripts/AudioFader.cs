using UnityEngine;
using System.Collections;

public class AudioFader : MonoBehaviour
{
    [Tooltip("The audio source to fade in and out")]
    [SerializeField] private AudioSource audioSource;

    [ Tooltip("The time it takes to fade in and out")]
    [SerializeField] private float fadeTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.volume = 0;
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }


    private IEnumerator FadeInCoroutine()
    {
        float startVolume = audioSource.volume;
        float endVolume = 1;
        float currentTime = 0;

        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, currentTime / fadeTime);
            yield return null;
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;
        float endVolume = 0;
        float currentTime = 0;

        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, currentTime / fadeTime);
            yield return null;
        }
    }
}
