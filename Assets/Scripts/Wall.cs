using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Shape _shape;
    private bool _move;

    [Header("Wall Properties")] [SerializeField]
    private float speed = 5;

    [SerializeField] private float delay = 15;
    [SerializeField] private int delayMin = 5;
    [SerializeField] private int repeatingRate = 3;

    [Header("Positions")] [SerializeField] private Transform screenTopPos;
    [SerializeField] private Transform screenBottomPos;

    [Header("Shapes")] [SerializeField] private List<Sprite> banners;
    [SerializeField] private SpriteRenderer bannerRenderer;

    [Header("References")] [SerializeField]
    private EnemyPool enemyPool;

    private float _timer;
    private int _counter;
    private readonly float _threshold = 0.1f;


    private void Update()
    {
        _timer = (_timer + Time.deltaTime) % delay;
        if (_timer < _threshold) StartWall();

        if (!_move) return;

        var moveVector = Vector3.MoveTowards(transform.position, screenBottomPos.position, speed * Time.deltaTime);
        transform.position = moveVector;
        if (transform.position == screenBottomPos.position) ResetWall();
    }

    private void StartWall()
    {
        _move = true;
        ChooseRandomShape();
    }

    private void StopWall()
    {
        _move = false;
    }

    private void ResetWall()
    {
        StopWall();
        transform.position = screenTopPos.position;
        UpdateWallProperties();

        enemyPool.SpawnEnemyGroup();
    }

    private void UpdateWallProperties()
    {
        _counter = (_counter + 1) % repeatingRate;
        if (_counter != 0) return;

        delay = Math.Max(delay - 1, delayMin);
    }


    private void ChooseRandomShape()
    {
        _shape = ShapeAccess.GetRandomShape();
        string layerName = "Wall" + _shape;

        bannerRenderer.sprite = _shape switch
        {
            Shape.Circle => banners[0],
            Shape.Square => banners[1],
            _ => banners[2]
        };

        gameObject.layer = LayerMask.NameToLayer(layerName);
    }
}