using UnityEngine;

namespace Entities
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Transform player;
        private Enemy _enemy;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void Update()
        {
            _enemy.LookAtPos(player.position);
        }
    }
}