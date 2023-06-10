using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private List<Transform> spawnPositions = new();
        [SerializeField] private int initialEnemyCount = 1;
        [SerializeField] private float initialShotInterval = 2;

        private float _shotInterval;
        private int _enemyCount;
        private const float MinimalShotInterval = 0.15f;
        private Vector3 _poolPos;
        private List<Transform> _enemies = new();
        private int _spawnCounter;

        private void Start()
        {
            _poolPos = transform.position;

            _shotInterval = initialShotInterval;
            _enemyCount = initialEnemyCount;

            InitEnemies();
            SpawnEnemyGroup();

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

            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.SetShotInterval(_shotInterval);
        }

        public void SpawnEnemyGroup()
        {
            ShuffleSpawnPoints();
            ShuffleEnemies();

            var cap = Math.Min(_enemyCount, _enemies.Count);
            var count = spawnPositions.Count;

            for (int i = 0; i < cap; i++)
            {
                var index = i % count;
                SpawnEnemy(spawnPositions[index].position);
            }

            UpdateEnemyProperties();
        }

        private void UpdateEnemyProperties()
        {
            // only do all 2 spawns, then set
            _spawnCounter = (_spawnCounter + 1) % 2;
            if (_spawnCounter != 0) return;

            // gradually increase number of spawned enemies and decrease shot interval
            _enemyCount++;
            _shotInterval = Math.Max(_shotInterval - 0.1f, MinimalShotInterval);
        }

        #region ListMethods

        private Transform Pop()
        {
            Transform toReturn = _enemies[0];
            _enemies.RemoveAt(0);
            return toReturn;
        }
        
        private void ShuffleSpawnPoints()
        {
            spawnPositions = spawnPositions.OrderBy(_ => Random.value).ToList();
        }

        private void ShuffleEnemies()
        {
            _enemies = _enemies.OrderBy(_ => Random.value).ToList();
        }

        #endregion
    }
}