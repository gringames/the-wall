using Entities;
using UnityEngine;

namespace Abilities
{
    public class DashAbility : Ability
    {
        [SerializeField] private Player _player;

        protected override void DoAbility()
        {
            _player.doubleSpeed = true;
        }
    }
}