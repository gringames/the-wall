using UnityEngine;
using UnityEngine.Events;
using Entities;

public class Doomhole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity != null)
        {
            entity.FallIntoVoid();
            return;
        }

        entity = collision.transform.parent.transform.parent.GetComponent<Entity>();
        if (entity != null)
        {
            entity.FallIntoVoid();
            return;
        }
    }
}
