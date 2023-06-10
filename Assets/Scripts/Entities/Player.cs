using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class Player : Entity
    {
        [SerializeField] List<GameObject> _shapes = new();
        [SerializeField] List<Weapon> _weapons = new();
        [SerializeField] private float maxLineDistance = 4f;
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