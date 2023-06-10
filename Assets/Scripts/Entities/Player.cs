using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class Player : Entity
    {
        [Header("Player Properties")] [SerializeField]
        List<GameObject> _shapes = new();

        [SerializeField] List<Weapon> _weapons = new();
        [SerializeField] private float speed = 20;
        [SerializeField] private float accelerationFactor = 5;

        [Header("Line Properties")] [SerializeField]
        private float maxLineDistance = 4f;

        [SerializeField] private Texture dot;
        [SerializeField] private float offset = 1.5f;

        // controlled by input system
        private LineRenderer _lineRenderer;
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.material.SetTexture(MainTex, dot);
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


        private void Move(Vector2 direction)
        {
            if (Frozen) return;

            direction = direction.normalized;
            Vector2 movVec = direction * speed;
            Rigidbody2D.velocity = Vector2.Lerp(Rigidbody2D.velocity, movVec, Time.deltaTime * accelerationFactor);
        }

        private void HandleInput()
        {
            Vector2 input = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                input.y = 1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                input.x = -1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                input.y = -1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                input.x = 1;
            }

            Move(input.normalized);

            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }

        private void SwitchShape()
        {
            if (Input.GetKey(KeyCode.Alpha1)) // Circle
            {
                SwitchShapeTo(Shape.Circle, 0);
            }
            else if (Input.GetKey(KeyCode.Alpha2)) // Square
            {
                SwitchShapeTo(Shape.Square, 1);
            }
            else if (Input.GetKey(KeyCode.Alpha3)) // Triangle
            {
                SwitchShapeTo(Shape.Triangle, 2);
            }
        }

        private void SwitchShapeTo(Shape newShape, int id)
        {
            shape = newShape;

            for (int i = 0; i < 3; i++)
            {
                _shapes[i].SetActive(i == id);
            }

            weapon = _weapons[id];

            string layerName = "PhysicsPlayer" + shape;
            gameObject.layer = LayerMask.NameToLayer(layerName);
        }

        private void DrawLine(Vector3 target)
        {
            var pos = transform.position + transform.right * offset;
            var direction = target - pos;

            bool mouseInsidePlayer = direction.magnitude < offset;
            _lineRenderer.enabled = !mouseInsidePlayer;

            direction = direction.normalized;

            var end = pos + direction * maxLineDistance;

            Vector3[] points = {pos, end};

            float width = _lineRenderer.startWidth;
            _lineRenderer.material.mainTextureScale = new Vector2(1f / width, 1.0f);

            _lineRenderer.SetPositions(points);
        }
    }
}