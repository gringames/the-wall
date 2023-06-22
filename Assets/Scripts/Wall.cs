using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Shape _shape;
    private bool _move;

    [Header("Wall Properties")] [SerializeField]
    private float speed = 5;

    [SerializeField] private float wallDelay = 15;
    [SerializeField] private float firstWallDelay = 30;
    [SerializeField] private int minimumWallDelay = 5;
    [SerializeField] private int repeatingRateForUpdatingProperties = 3;

    [Header("Positions")] [SerializeField] private Transform screenTopPos;
    [SerializeField] private Transform screenBottomPos;

    [Header("Shapes")] [SerializeField] private List<Sprite> banners;
    [SerializeField] private SpriteRenderer bannerRenderer;

    [Header("Enemy Spawning")] [SerializeField]
    private EnemyPool enemyPool;

    [SerializeField] private float spawnDelay = 2;

    private float _timer;
    private int _counter;
    private readonly float _threshold = 0.1f;
    private bool _startTimer;

    public void EnableWall()
    {
        gameObject.SetActive(true);
        Invoke(nameof(StartTimer), firstWallDelay);
    }

    private void StartTimer()
    {
        _startTimer = true;
    }


    private void Update()
    {
        if (!_startTimer) return;

        _timer = (_timer + Time.deltaTime) % wallDelay;
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

        StartCoroutine(nameof(WaitAndSpawnNewEnemies));
    }

    private IEnumerator WaitAndSpawnNewEnemies()
    {
        yield return new WaitForSeconds(spawnDelay);
        enemyPool.SpawnEnemyGroup();
    }

    private void UpdateWallProperties()
    {
        _counter = (_counter + 1) % repeatingRateForUpdatingProperties;
        if (_counter != 0) return;

        wallDelay = Math.Max(wallDelay - 1, minimumWallDelay);
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