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
            
            string layerName = "PhysicsEnemy" + shape;
            gameObject.layer = LayerMask.NameToLayer(layerName);
            
            InvokeRepeating(nameof(Shoot), _shotInterval, _shotInterval);
        }

        private void Update()
        {
            LookAtPos(player.position);
            if (IsDoneFalling)
            {
                _pool.ResetEnemy(transform);
                IsDoneFalling = false;
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