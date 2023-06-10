using UnityEngine;

namespace Abilities
{
    public class GrappleAbility : Ability
    {
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
                ResetAbility();
                return;
            }
            
            _grapple.Fire();
        }

        public void SetAttacked(Transform attacked)
        {
            _attackedEnemy = attacked;
            _attacked = true;
        }

        private void ResetAbility()
        {
            _attackedEnemy = null;
            _attacked = false;
            _grapple.ResetGrapple();
        }




    }
}