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
        private readonly float _scaleSpeed = 0.05f;
        private readonly float _scaleMultiplier = 0.1f;

        [SerializeField] private float knockbackResistance = 2;

        [SerializeField] private Ability ability;
        [SerializeField] protected Weapon weapon;
        private Rigidbody2D _rigidbody2D;

        private bool _freezed;
        private readonly float _freezeTime = 1f;


        #region Init

        private void Awake()
        {
            InitRigidbody();
        }

        private void InitRigidbody()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.drag = knockbackResistance;
        }

        #endregion

        public void LookAtPos(Vector3 targetPos)
        {
            Vector3 lookAt = targetPos;
            float angleRad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);
            float angleDeg = 180 / Mathf.PI * angleRad;
            transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }

        #region Fall

        protected void FallIntoVoid()
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

            while (transform.localScale.x > scale.x)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, scale, _scaleSpeed);
                yield return new WaitForFixedUpdate();
            }
        }

        #endregion

        protected void GetKnocked(Vector2 direction, float strength)
        {
            // strength 5 was used

            Freeze();

            var force = direction * strength;
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);

            Invoke(nameof(Unfreeze), _freezeTime);
        }

        #region Freeze

        private void Freeze()
        {
            _freezed = true;
        }

        private void Unfreeze()
        {
            _freezed = false;
        }

        #endregion


        #region Actions

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
            ability.Perform();
        }

        #endregion
    }
}