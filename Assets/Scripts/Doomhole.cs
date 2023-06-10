using UnityEngine;
using UnityEngine.Events;
using Entities;

public class Doomhole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Projectile>(out var projectile))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<Entity>(out var entity))
        {
            entity.FallIntoVoid();
            return;
        }

        Debug.Log(collision);

        if (collision.transform.parent.transform.parent.TryGetComponent<Entity>(out entity))
        {
            entity.FallIntoVoid();
            return;
        }
    }
}
