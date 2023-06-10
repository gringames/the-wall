using Entities;
using UnityEngine;

namespace Abilities
{
    public class DashAbility : Ability
    {
        [SerializeField] private Player _player;
        [SerializeField] private float _speedBoost = 2f;
        [SerializeField] private float _abilityDuration = 0.5f;

        protected override void DoAbility()
        {
            _player.Speed *= _speedBoost;
            Invoke("StopAbility", _abilityDuration);
        }

        private void StopAbility()
        {
            _player.Speed /= _speedBoost;
        }
    }
}