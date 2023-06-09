using System;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        private float _cooldown;

        private void Awake()
        {
            _cooldown = cooldown;
        }

        public void Perform()
        {
            if (_cooldown <= 0) return;
            DoAbility();
        }

        protected abstract void DoAbility();
    }
}