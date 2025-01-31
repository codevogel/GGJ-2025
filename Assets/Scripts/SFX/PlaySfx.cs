using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySfx : MonoBehaviour
{
    // singelton
    public static PlaySfx instance;

    AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(this);
        }
    }

    /**
     @param sfx: the sound effect to play
     @param pos: the position to play the sound effect
     @param minVolume: the minimum volume of the sound effect
     @param maxVolume: the maximum volume of the sound effect
     @param minPitch: the minimum pitch of the sound effect
     @param maxPitch: the maximum pitch of the sound effect
     */

    public void playOneShotSFX(AudioClip sfx, Transform pos, float minVolume = 0.8f, float maxVolume = 1.0f, float minPitch = 0.8f, float maxPitch = 1.2f, float spatialBlend = 1f)
    {
        if (sfx == null)
        {
            Debug.LogWarning("No SFX found");
            return;
        }

        if (pos == null)
        {
            Debug.LogWarning("No position found");
            return;
        }

        float volume = Random.Range(minVolume, maxVolume);
        float pitch = Random.Range(minPitch, maxPitch);
        audioSource.spatialBlend = spatialBlend;

        audioSource.pitch = pitch;

        AudioSource.PlayClipAtPoint(sfx, pos.position, volume);
    }

}