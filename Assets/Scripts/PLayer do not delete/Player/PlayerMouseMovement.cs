using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMovement : Character
{
    private Vector3 targetPos;
    private Camera mainCamera;

    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
    }


    protected override void Update()
    {
        GetInput();     
        MoveToTargetPos();
        Move();
        base.HandleLayers();    
    }
    private  void Move()
    {
        var diff = targetPos - transform.position;
        Debug.Log(diff.x+"y"+diff.y);
        if (diff.y > 0 && (diff.x < 0.1 || diff.x < -0.1))
        {
            direction += Vector2.up;
        }
        else if (diff.y < 0 && (diff.x < 0.1 || diff.x < -0.1))
        {
            direction += Vector2.down;
        }
        else if (diff.x > 0 && (diff.y < 0.1 || diff.y < -0.1))
        {
            direction += Vector2.right;
        }
        else if (diff.x < 0 && (diff.y < 0.1 || diff.y < -0.1))
        {
            direction += Vector2.left;
        }
    }
    private void GetInput()
    {
        direction = Vector2.zero;
       
        if (Input.GetMouseButtonDown(0))
        {
            CalculateTargetPosition();
            Debug.Log(targetPos);           
        }

    }

    void CalculateTargetPosition()
    {
        var mousePos = Input.mousePosition;
        var transformPos = mainCamera.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transformPos.x, transformPos.y, 0);
    }
    private void MoveToTargetPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);
    }
}
