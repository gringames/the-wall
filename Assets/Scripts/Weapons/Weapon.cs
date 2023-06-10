using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float _cooldown = 0.75f;
        [SerializeField] private int _projectileCount = 1;
        [SerializeField] private float _strayAngle = 0f;
        [SerializeField] private List<GameObject> _projectileSpawnPoints;
        [SerializeField] private GameObject _projectilePrefab;

        [SerializeField] private List<GameObject> _barrels;

        private float _cooldownProgress = 0f;
        private int _spawnIndex = 0;

        public void Shoot()
        {
            if (_cooldownProgress < _cooldown) return;
            _cooldownProgress = 0;

            for (int i = 0; i < _projectileCount; i++)
            {
                GameObject projectile = Instantiate(_projectilePrefab);
                GameObject spawn = _projectileSpawnPoints[_spawnIndex];
                projectile.transform.SetPositionAndRotation(spawn.transform.position, spawn.transform.rotation);
                projectile.transform.rotation *= Quaternion.Euler(0, 0, Random.Range(-_strayAngle, _strayAngle));
                _spawnIndex++;
                if (_spawnIndex >= _projectileSpawnPoints.Count)
                {
                    _spawnIndex = 0;
                }
            }
        }

        private void Update()
        {
            _cooldownProgress += Time.deltaTime;
        }
    }
}