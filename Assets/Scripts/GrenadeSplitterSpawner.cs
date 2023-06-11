using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSplitterSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int amount = 15;

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            transform.rotation = Random.rotation;
            GameObject instance = Instantiate(prefab);
            instance.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
