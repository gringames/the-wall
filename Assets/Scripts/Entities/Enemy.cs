using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        [SerializeField] private Transform player;
        private EnemyPool _pool;

        private void Start()
        {
            _pool = GetComponentInParent<EnemyPool>();
        }

        private void Update()
        {
            LookAtPos(player.position);
        }

        protected override void FallIntoVoid()
        {
            base.FallIntoVoid();
            _pool.ResetEnemy(transform);
        }
    }
}