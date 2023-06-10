using System.Collections;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        protected bool Ready;

        private void Awake()
        {
            Ready = true;
        }

        public void Perform()
        {
            if (!Ready) return;

            DoAbility();
            StartCoroutine(nameof(StartCooldownTimer));
        }

        private IEnumerator StartCooldownTimer()
        {
            Ready = false;
            yield return new WaitForSeconds(cooldown);
            Ready = true;
        }


        protected abstract void DoAbility();
    }
}