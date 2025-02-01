using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Tooltip("The audio source for when the player bubble is colliding with another player bubble")]
    [SerializeField] private AudioClip[] B2bCollisions;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlaySfx.instance.playOneShotSFX(B2bCollisions[Random.Range(0, B2bCollisions.Length)], transform, 0.8f, 1.0f, 0.8f, 1.2f, 1f);
        }
    }

}
