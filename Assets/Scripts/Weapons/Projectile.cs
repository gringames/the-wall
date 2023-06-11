using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] int _maxBounces = 1;
    [SerializeField] float _lifetime = -1f;

    [SerializeField] GameObject _deathPrefab;

    private int _bounces = 0;

    private void Start()
    {
        if (_lifetime > 0)
        {
            Invoke("Death", _lifetime);
        }
    }

    void Update()
    {
        _rb.velocity = transform.right * _speed;
        _rb.angularVelocity = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_bounces >= _maxBounces)
        {
            if (_deathPrefab != null)
            {
                GameObject death = Instantiate(_deathPrefab);
                death.transform.SetPositionAndRotation(transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        transform.right = Vector2.Reflect(transform.right, collision.GetContact(0).normal);
        _bounces++;
    }

    private void Death()
    {
        if (_deathPrefab != null)
        {
            GameObject death = Instantiate(_deathPrefab);
            death.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
