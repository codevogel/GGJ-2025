using UnityEngine;
using System.Collections;

public class victoryMusic : MonoBehaviour
{
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip victoryMusicIntro;
    [SerializeField] private AudioClip victoryMusicLoop;

    private void Start()
    {
        StartCoroutine(playVictoryMusic());
    }

    IEnumerator playVictoryMusic()
    {
        audioSource.clip = victoryMusicIntro;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(victoryMusicIntro.length);
        audioSource.clip = victoryMusicLoop;
        audioSource.loop = true;
        audioSource.Play();
    }
}
