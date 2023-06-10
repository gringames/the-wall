using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class GrappleAbility : Ability
    {
        [SerializeField] private float pullStrength = 1;
        private Transform _attackedEnemy;
        private Grapple _grapple;
        
        private bool _attacked;


        private void Start()
        {
            _grapple = GetComponentInChildren<Grapple>();
        }

        protected override void DoAbility()
        {
            if (_attacked)
            {
                PullEnemy();
                return;
            }
            
            _grapple.Fire();
        }

        public void SetAttacked(Transform attacked)
        {
            _attackedEnemy = attacked;
            _attacked = true;
        }


        private void PullEnemy()
        {
            var dir = transform.position - _grapple.transform.position;
            var enemyRB = _attackedEnemy.GetComponent<Rigidbody2D>();
            
            enemyRB.AddForce(dir.normalized * pullStrength, ForceMode2D.Impulse);
            
            StartCoroutine(nameof(WaitAndReset));
        }

        private IEnumerator WaitAndReset()
        {
            yield return new WaitForSeconds(1);
            ResetAbility();
            
        }

        private void ResetAbility()
        {
            _attackedEnemy = null;
            _attacked = false;
            _grapple.ResetGrapple();
        }




    }
}