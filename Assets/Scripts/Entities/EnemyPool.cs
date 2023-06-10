using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public class EnemyPool : MonoBehaviour
    {
        private Vector3 _poolPos;

        private List<Transform> _enemies = new List<Transform>();
        [SerializeField] private List<Transform> spawnPositions = new List<Transform>();


        private void Start()
        {
            _poolPos = transform.position;
            
            InitEnemies();
            
            SpawnEnemy();
        }

        private void InitEnemies()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _enemies.Add(transform.GetChild(i));
            }
        }

        public void ResetEnemy(Transform enemy)
        {
            enemy.position = _poolPos;
            enemy.localScale = new Vector3(1, 1);
            enemy.gameObject.SetActive(false);
            _enemies.Add(enemy);

            SpawnEnemy();
        }

        public void SpawnEnemy()
        {
            Transform enemy = Pop();
            enemy.gameObject.SetActive(true);
            enemy.position = GetRandomSpawnPos();
        }

        private Transform Pop()
        {
            Transform toReturn = _enemies[0];
            _enemies.RemoveAt(0);
            return toReturn;
        }

        private Vector3 GetRandomSpawnPos()
        {
            int index = Random.Range(0, spawnPositions.Count);
            return spawnPositions[index].position;
        }
    }
}