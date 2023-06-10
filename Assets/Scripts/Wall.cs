using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Shape _shape;
    private bool _move;
    [SerializeField] private Transform screenTopPos;
    [SerializeField] private Transform screenBottomPos;
    [SerializeField] private float speed = 5;
    [SerializeField] private float repeatRate = 15;

    [SerializeField] private List<Sprite> _banners;
    [SerializeField] private SpriteRenderer _bannerRenderer;

    private void Awake()
    {
        // TODO: dynamic
        InvokeRepeating(nameof(StartWall), 1, repeatRate);
    }

    private void Update()
    {
        if (!_move) return;

        var moveVector = Vector3.MoveTowards(transform.position, screenBottomPos.position, speed * Time.deltaTime);
        transform.position = moveVector;

        if (transform.position == screenBottomPos.position)
        {
            ResetWall();
        }
    }

    public void StartWall()
    {
        _move = true;
        ChooseRandomShape();
    }

    public void StopWall()
    {
        _move = false;
    }

    private void ResetWall()
    {
        StopWall();
        transform.position = screenTopPos.position;
    }


    private void ChooseRandomShape()
    {
        _shape = ShapeAccess.GetRandomShape();
        string layerName = "Wall" + _shape;

        if (_shape == Shape.Circle)
        {
            _bannerRenderer.sprite = _banners[0];
        }
        else if (_shape == Shape.Square)
        {
            _bannerRenderer.sprite = _banners[1];
        }
        else
        {
            _bannerRenderer.sprite = _banners[2];
        }

        gameObject.layer = LayerMask.NameToLayer(layerName);
    }
}