using UnityEngine;

namespace Abilities
{
    public class GrenadeAbility : Ability
    {
        [SerializeField] private GameObject muzzle;
        [SerializeField] private GameObject grenadePrefab;

        protected override void DoAbility()
        {
            GameObject projectile = Instantiate(grenadePrefab);
            projectile.transform.SetPositionAndRotation(muzzle.transform.position, muzzle.transform.rotation);
        }
    }
}