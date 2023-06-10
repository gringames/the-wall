using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class Player : Entity
    {
        [SerializeField] List<GameObject> _shapes = new();
        [SerializeField] List<Weapon> _weapons = new();

        // controlled by input system
        private LineRenderer _lineRenderer;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            
        }


        private void Update()
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            LookAtPos(mouseWorldPos);
            HandleInput();
            SwitchShape();
            DrawLine(mouseWorldPos);
        }

        private void HandleInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                Move(Vector2.up);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Move(Vector2.left);
            }

            if (Input.GetKey(KeyCode.S))
            {
                Move(Vector2.down);
            }

            if (Input.GetKey(KeyCode.D))
            {
                Move(Vector2.right);
            }

            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }

        private void SwitchShape()
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                _shapes[0].SetActive(true);
                _shapes[1].SetActive(false);
                _shapes[2].SetActive(false);
                weapon = _weapons[0];
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                _shapes[0].SetActive(false);
                _shapes[1].SetActive(true);
                _shapes[2].SetActive(false);
                weapon = _weapons[1];
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                _shapes[0].SetActive(false);
                _shapes[1].SetActive(false);
                _shapes[2].SetActive(true);
                weapon = _weapons[2];
            }
        }

        private void DrawLine(Vector3 target)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, target);
        }
    }
}