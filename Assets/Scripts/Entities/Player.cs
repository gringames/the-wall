using UnityEngine;

namespace Entities
{
    public class Player : Entity
    {
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

        private void DrawLine(Vector3 target)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, target);
        }
    }
}