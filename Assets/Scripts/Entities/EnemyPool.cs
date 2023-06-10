using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private List<Transform> spawnPositions = new List<Transform>();

        private Vector3 _poolPos;
        private List<Transform> _enemies = new List<Transform>();
        [SerializeField] private int initialEnemyCount = 1;
        private int _spawnCounter;


        private void Start()
        {
            _poolPos = transform.position;

            InitEnemies();
            SpawnEnemyGroup(initialEnemyCount);
            
            // TODO: sub to wall event: SpawnGroup
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
        }

        private void SpawnEnemy(Vector3 position)
        {
            Transform enemy = Pop();
            enemy.gameObject.SetActive(true);
            enemy.position = position;
        }

        private void SpawnEnemyGroup(int numberOfEnemies)
        {
            ShuffleSpawnPoints();

            var cap = Math.Min(numberOfEnemies, _enemies.Count);
            var count = spawnPositions.Count;

            for (int i = 0; i < cap; i++)
            {
                var index = i % count;
                SpawnEnemy(spawnPositions[index].position);
            }

            // gradually increase number of spawned enemies
            _spawnCounter++;
            if (_spawnCounter <= 2) return;
            
            initialEnemyCount++;
            _spawnCounter = 0;
        }

        #region ListMethods

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

        private void ShuffleSpawnPoints()
        {
            spawnPositions = spawnPositions.OrderBy(_ => Random.value).ToList();
        }

        #endregion
    }
}