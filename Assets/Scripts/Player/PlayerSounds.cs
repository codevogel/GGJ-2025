using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Tooltip("The audio for when the player bubble is colliding with another player bubble")]
    [SerializeField] private AudioClip[] B2bCollisions;

    [Tooltip("The audio for when a players bubble bursts")]
    [SerializeField] private AudioClip[] Burst;

    [Tooltip ("The audio for when a player picks up a bubble")]
    [SerializeField] private AudioClip[] Pickup;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlaySfx.instance?.playOneShotSFX(B2bCollisions[Random.Range(0, B2bCollisions.Length)], transform, 0.8f, 1.0f, 0.8f, 1.2f, 1f);
        }
    }

    public void onDeath()
    {
        PlaySfx.instance?.playOneShotSFX(Burst[Random.Range(0, Burst.Length)], transform, 0.8f, 1.0f, 0.8f, 1.2f, 1f);
    }

    public void onPickp()
    {
        PlaySfx.instance?.playOneShotSFX(Pickup[Random.Range(0, Pickup.Length)], transform, 0.8f, 1f, 0.8f, 1.2f, 0f);
    }

}
