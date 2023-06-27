using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons;
using Abilities;

namespace Entities
{
    public class Player : Entity
    {
        [Header("Player Properties")] [SerializeField]
        List<GameObject> _shapes = new();

        [SerializeField] List<Weapon> _weapons = new();
        [SerializeField] List<Ability> _abilities = new();
        [SerializeField] private float speed = 20;
        [SerializeField] private float accelerationFactor = 5;

        [Header("Line Properties")] [SerializeField]
        private float maxLineDistance = 4f;

        [SerializeField] private Texture dot;
        [SerializeField] private float offset = 1.5f;

        // controlled by input system
        private LineRenderer _lineRenderer;
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        public bool doubleSpeed = false;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.material.SetTexture(MainTex, dot);
        }


        private void Update()
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            Rigidbody2D.angularVelocity = 0;

            LookAtPos(mouseWorldPos);
            HandleInput();
            SwitchShape();
            DrawLine(mouseWorldPos);
        }


        private void Move(Vector2 direction, bool forceUpdate = false)
        {
            if (Frozen) return;

            if (forceUpdate)
            {
                Rigidbody2D.velocity = direction * speed;   
            }
            else
            {
                Vector2 movVec = direction * speed;
                Rigidbody2D.velocity = Vector2.Lerp(Rigidbody2D.velocity, movVec, Time.deltaTime * accelerationFactor);
            }
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

            if (doubleSpeed)
            {
                Move(transform.right * 2, true);
                doubleSpeed = false;
            }
            else
            {
                Move(input.normalized);
            }

            if (Input.GetMouseButton(0))
            {
                Shoot();
            }

            if (Input.GetMouseButton(1))
            {
                PerformAbility();
            }
        }

        private void SwitchShape()
        {
            if (Input.GetKey(KeyCode.Alpha1)) // Circle
            {
                SwitchShapeTo(Shape.Circle, 0);
                MusicManager.Instance.SetCharacter(MusicManager.Character.Circle);
            }
            else if (Input.GetKey(KeyCode.Alpha2)) // Square
            {
                SwitchShapeTo(Shape.Square, 1);
                MusicManager.Instance.SetCharacter(MusicManager.Character.Square);
            }
            else if (Input.GetKey(KeyCode.Alpha3)) // Triangle
            {
                SwitchShapeTo(Shape.Triangle, 2);
                MusicManager.Instance.SetCharacter(MusicManager.Character.Triangle);
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
            ability = _abilities[id];

            string layerName = "PhysicsPlayer" + shape;
            //gameObject.layer = LayerMask.NameToLayer(layerName);
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

        public override void FallIntoVoid()
        {
            base.FallIntoVoid();
            Invoke("GameOver", 1);
        }

        private void GameOver()
        {
            GameData.Instance.Reload();
            SceneManager.LoadScene(0);
        }
    }
}