using Abilities;
using UnityEngine;
using Weapons;

// do object pooling with enemies and bullets


namespace Entities
{
    //[RequireComponent(typeof(Ability))] [RequireComponent(typeof(Rigidbody2D))]
    public class Entity : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float knockbackResistance;

        private Ability _ability;
        [SerializeField] private Weapon weapon;
        private Rigidbody2D _rigidbody2D;

        private bool _freezed;

        #region Init

        private void Awake()
        {
            // InitAbility();
            // InitWeapon();
            InitRigidbody();
        }

        private void InitAbility()
        {
            _ability = GetComponent<Ability>();
        }

        private void InitWeapon()
        {
            weapon = GetComponent<Weapon>();
        }

        private void InitRigidbody()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        #endregion


        #region Actions

        private void FallIntoVoid()
        {
            Freeze();
            ShrinkDown();
        }

        private void Freeze()
        {
            _freezed = true;
        }

        private void ShrinkDown()
        {
            // TODO: call Coroutine that slowly decreases scale of transform
        }

        private void GetKnocked(Vector2 direction, float strength)
        {
            // TODO: implement
            // different strength for hitting wall or other enemy
        }

        protected void Move(Vector2 direction)
        {
            if (_freezed) return;

            direction = direction.normalized;
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);

            Vector2 movVec = direction * (speed * Time.deltaTime);
            _rigidbody2D.MovePosition(pos + movVec);
        }

        protected void Shoot()
        {
            weapon.Shoot();
        }

        private void PerformAbility()
        {
            _ability.Perform();
        }

        #endregion
    }
}