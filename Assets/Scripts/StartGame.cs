using Entities;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject spawner;


    void Start()
    {
        wall.SetActive(false);
        spawner.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.transform.parent.TryGetComponent<Entity>(out Entity entity))
        {
            Invoke("Play", 1.5f);
            GameData.Instance.StartGame();
            gameObject.SetActive(false);
        }
    }

    private void Play()
    {
        wall.GetComponent<Wall>().EnableWall();
        spawner.SetActive(true);
    }
}
