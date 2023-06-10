using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        [SerializeField] private Transform player;
        private EnemyPool _pool;
        private float _shotInterval;

        private void Start()
        {
            _pool = GetComponentInParent<EnemyPool>();
            
            InvokeRepeating(nameof(Shoot), _shotInterval, _shotInterval);
        }

        private void Update()
        {
            LookAtPos(player.position);
            if (_isDoneFalling)
            {
                _pool.ResetEnemy(transform);
                _isDoneFalling = false;
            }
        }

        public override void FallIntoVoid()
        {
            base.FallIntoVoid();
        }

        public void SetShotInterval(float interval)
        {
            _shotInterval = interval;
        }
    }
}