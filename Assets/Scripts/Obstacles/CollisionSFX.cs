using UnityEngine;

public class CollisionSFX : MonoBehaviour
{
    [SerializeField] private AudioClip _collisionSound;

    private bool hitGround = false;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && !hitGround)
        {
            hitGround = true;
            PlaySfx.instance?.playOneShotSFX(_collisionSound, transform, 0.8f, 1.0f, 0.8f, 1.2f, 0f);
        }
    }

}
