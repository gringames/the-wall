using System.Collections;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        [SerializeField] private AudioSource _audio;
        protected bool Ready;

        private void Awake()
        {
            Ready = true;
        }

        public void Perform()
        {
            if (!Ready) return;
            _audio.Play();
            Ready = false;
            DoAbility();
            Invoke(nameof(ReadyAbility), cooldown);
        }

        private void ReadyAbility()
        {
            Ready = true;
        }


        protected abstract void DoAbility();
    }
}