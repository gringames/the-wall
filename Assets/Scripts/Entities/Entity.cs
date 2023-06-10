using System.Collections;
using Abilities;
using UnityEngine;
using Weapons;

// do object pooling with enemies and bullets


namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Entity : MonoBehaviour
    {
        private readonly float _scaleSpeed = 0.05f;
        private readonly float _scaleMultiplier = 0.1f;

        [SerializeField] private float knockBackResistance = 2;

        [SerializeField] protected Ability ability;
        [SerializeField] protected Weapon weapon;
        protected Rigidbody2D Rigidbody2D;

        protected bool IsDoneFalling = false;
        protected bool Frozen;

        [SerializeField] protected Shape shape;

        #region Init

        private void Awake()
        {
            InitRigidbody();
            Unfreeze();
        }

        private void InitRigidbody()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Rigidbody2D.drag = knockBackResistance;
        }

        #endregion

        #region Fall

        public virtual void FallIntoVoid()
        {
            Freeze();
            ShrinkDown();
        }


        private void ShrinkDown()
        {
            StartCoroutine(nameof(ShrinkOverTime));
        }

        private IEnumerator ShrinkOverTime()
        {
            var scale = transform.localScale;
            scale *= _scaleMultiplier;

            while (Rigidbody2D.velocity != Vector2.zero)
            {
                Rigidbody2D.velocity = Vector2.MoveTowards(Rigidbody2D.velocity, Vector2.zero, Mathf.Max(_scaleSpeed, _scaleSpeed * Rigidbody2D.velocity.magnitude) * 60);
                yield return new WaitForFixedUpdate();
            }

            while (transform.localScale.x > scale.x)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, scale, _scaleSpeed);
                yield return new WaitForFixedUpdate();
            }
            IsDoneFalling = true;
        }

        #endregion

        #region Freeze

        private void Freeze()
        {
            Frozen = true;
        }

        private void Unfreeze()
        {
            Frozen = false;
        }

        #endregion

        #region Actions

        protected void LookAtPos(Vector3 targetPos)
        {
            Vector3 lookAt = targetPos;
            float angleRad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);
            float angleDeg = 180 / Mathf.PI * angleRad;
            transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }


        protected void Shoot()
        {
            weapon.Shoot();
        }

        protected void PerformAbility()
        {
            ability.Perform();
        }

        #endregion
    }
}