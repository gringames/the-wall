using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _knockbackForce = 10f;
    [SerializeField] Rigidbody2D _rb;

    void Update()
    {
        _rb.velocity = transform.right * _speed;
    }
}
