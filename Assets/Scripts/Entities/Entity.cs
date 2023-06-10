using Abilities;
using UnityEngine;
using Weapons;

// do object pooling with enemies and bullets


namespace Entities
{
    [RequireComponent(typeof(Ability))] [RequireComponent(typeof(Rigidbody2D))]
    public class Entity : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float knockbackResistance;

        private Ability _ability;
        private Weapon _weapon;
        private Rigidbody2D _rigidbody;

        #region Init

        private void Awake()
        {
            InitAbility();
            InitWeapon();
            InitRigidbody();
        }

        private void InitAbility()
        {
            _ability = GetComponent<Ability>();
        }

        private void InitWeapon()
        {
            _weapon = GetComponent<Weapon>();
        }

        private void InitRigidbody()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
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
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void ShrinkDown()
        {
            // TODO: call Coroutine that slowly decreases scale of transform
        }

        private void GetKnocked(Vector2 direction, float strength)
        {
            // TODO: implement with RB2D
            // different strength for hitting wall or other enemy
        }

        private void Move(Vector2 direction)
        {
            // TODO: maybe do with rigidbody
            Vector3 direction3D = new Vector3(direction.x, direction.y, 0);
            transform.position += direction3D;
        }

        private void Shoot()
        {
            _weapon.Shoot();
        }

        private void PerformAbility()
        {
            _ability.Perform();
        }

        #endregion
    }
}