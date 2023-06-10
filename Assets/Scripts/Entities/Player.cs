using UnityEngine;

namespace Entities
{
    public class Player : Entity
    {
        // controlled by input system
        private LineRenderer _LineRenderer;

        private void Start()
        {
            _LineRenderer = GetComponent<LineRenderer>();
        }


        private void Update()
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            LookAtMouse(mouseWorldPos);
            HandleInput();
            DrawLine(mouseWorldPos);
        }

        private void LookAtMouse(Vector3 mousePos)
        {
            Vector3 lookAt = mousePos;
            float AngleRad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);
            float AngleDeg = 180 / Mathf.PI * AngleRad;
            transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
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
            _LineRenderer.SetPosition(0, transform.position);
            _LineRenderer.SetPosition(1, target);
        }
    }
}