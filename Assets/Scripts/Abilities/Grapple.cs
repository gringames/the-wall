using UnityEngine;

namespace Abilities
{
    public class Grapple : MonoBehaviour
    {
        [SerializeField] private float speed = 6;
        [SerializeField] private float maxRange = 7;

        private GrappleAbility _grappleAbility;
        private Vector3 _grapplePos;
        private bool _fired;

        private void Start()
        {
            _grappleAbility = GetComponentInParent<GrappleAbility>();
        }
        
        private void Update()
        {
            if (!_fired) return;

            transform.position += Vector3.right * (speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, _grapplePos) > maxRange)
            {
                ResetGrapple();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Transform enemy = other.transform;
            _grappleAbility.SetAttacked(enemy);
            transform.parent = enemy;
        }

        public void Fire()
        {
            _fired = true;
            transform.parent = null;
            _grapplePos = transform.position;
        }
        
        public void ResetGrapple()
        {
            _fired = false;
            transform.parent = _grappleAbility.transform;
            transform.position = transform.parent.position;
        }
    }
}