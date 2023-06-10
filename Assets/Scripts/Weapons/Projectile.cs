using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _knockbackForce = 10f;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] int _maxBounces = 1;

    private int _bounces = 0;

    void Update()
    {
        _rb.velocity = transform.right * _speed;
        _rb.angularVelocity = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_bounces >= _maxBounces)
        {
            Destroy(gameObject);
        }
        transform.right = Vector2.Reflect(transform.right, collision.GetContact(0).normal);
        _bounces++;
    }
}
