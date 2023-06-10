using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Shape _shape;
    private bool _move;
    [SerializeField] private Vector3 screenBottomPos;
    [SerializeField] private Vector3 screenTopPos;
    [SerializeField] private float speed;

    private void Awake()
    {
        // gameObject.layer = LayerMask.NameToLayer("");
    }

    private void Update()
    {
        if (!_move) return;

        var moveVector = Vector3.MoveTowards(transform.position, screenBottomPos, speed * Time.deltaTime);
        transform.position = moveVector;

        if (transform.position == screenBottomPos)
        {
            ResetWall();
        }
    }

    public void StartWall()
    {
        _move = true;
    }

    public void StopWall()
    {
        _move = false;
    }

    private void ResetWall()
    {
        StopWall();
        transform.position = screenTopPos;
    }


    private void ChooseRandomShape()
    {
        _shape = ShapeAccess.GetRandomShape();
    }


}