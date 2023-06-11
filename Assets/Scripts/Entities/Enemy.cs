using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        [SerializeField] private Transform player;
        private EnemyPool _pool;
        private float _shotInterval;

        private float _shotTime = 0;

        private void Start()
        {
            _pool = GetComponentInParent<EnemyPool>();

            _shotTime -= Random.value * _shotInterval / 2;
        }

        private void Update()
        {
            LookAtPos(player.position);

            _shotTime += Time.deltaTime;
            if (_shotTime >= _shotInterval)
            {
                Shoot();
                if (gameObject.name.Contains("Square"))
                {
                    if (Random.value > 0.5)
                    {
                        _shotTime -= 0.25f;
                    }
                    else
                    {
                        _shotTime = 0;
                    }
                }
                else if (gameObject.name.Contains("Triangle"))
                {
                    _shotTime -= _shotTime * 1.5f;
                }
                else
                {
                    _shotTime = 0;
                }
            }

            if (IsDoneFalling)
            {
                _pool.ResetEnemy(transform);
                IsDoneFalling = false;
            }
        }

        public void SetShotInterval(float interval)
        {
            _shotInterval = interval;
        }
    }
}