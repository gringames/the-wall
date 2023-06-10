using System.Collections.Generic;
using Entities;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Shape _shape;
    private bool _move;
    
    [Header("Wall Properties")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float repeatRate = 15;
    
    [Header("Positions")]
    [SerializeField] private Transform screenTopPos;
    [SerializeField] private Transform screenBottomPos;

    [Header("Shapes")]
    [SerializeField] private List<Sprite> banners;
    [SerializeField] private SpriteRenderer bannerRenderer;

    [Header("References")]
    [SerializeField] private EnemyPool _enemyPool;

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
        _enemyPool.SpawnEnemyGroup();
    }


    private void ChooseRandomShape()
    {
        _shape = ShapeAccess.GetRandomShape();
        string layerName = "Wall" + _shape;

        if (_shape == Shape.Circle)
        {
            bannerRenderer.sprite = banners[0];
        }
        else if (_shape == Shape.Square)
        {
            bannerRenderer.sprite = banners[1];
        }
        else
        {
            bannerRenderer.sprite = banners[2];
        }

        gameObject.layer = LayerMask.NameToLayer(layerName);
    }
}