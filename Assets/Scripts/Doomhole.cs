using UnityEngine;
using UnityEngine.Events;
using Entities;

public class Doomhole : MonoBehaviour
{
    [SerializeField] private AudioSource _deathAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Projectile>(out var projectile))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<Entity>(out var entity))
        {
            _deathAudio.Play();
            entity.FallIntoVoid();
            return;
        }

        if (collision.transform.parent.transform.parent.TryGetComponent<Entity>(out entity))
        {
            _deathAudio.Play();
            entity.FallIntoVoid();
            return;
        }
    }
}
