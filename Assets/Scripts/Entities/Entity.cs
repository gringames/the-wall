using System.Collections;
using Abilities;
using UnityEngine;
using Weapons;

// do object pooling with enemies and bullets


namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Entity : MonoBehaviour
    {
        [SerializeField] private float speed = 20;
        [SerializeField] private float _accelerationFactor = 5;
        private readonly float _scaleSpeed = 0.05f;
        private readonly float _scaleMultiplier = 0.1f;

        [SerializeField] private float knockBackResistance = 2;

        [SerializeField] private Ability ability;
        [SerializeField] protected Weapon weapon;
        private Rigidbody2D _rigidbody2D;

        #region Init

        private void Awake()
        {
            InitRigidbody();
        }

        private void InitRigidbody()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.drag = knockBackResistance;
        }

        #endregion

        #region Fall

        // TODO: call in LOCH
        protected virtual void FallIntoVoid()
        {
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

            while (transform.localScale.x > scale.x)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, scale, _scaleSpeed);
                yield return new WaitForFixedUpdate();
            }
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

        protected void Move(Vector2 direction)
        {
            direction = direction.normalized;
            Vector2 movVec = direction * speed;
            _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, movVec, Time.deltaTime * _accelerationFactor);
        }

        protected void Shoot()
        {
            weapon.Shoot();
        }

        private void PerformAbility()
        {
            ability.Perform();
        }

        #endregion
    }
}