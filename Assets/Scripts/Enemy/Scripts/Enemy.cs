using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;


public class Enemy : NPC
{
    private Transform target;
    
    private IEnemyState currentState;

    

    private bool reachedPathEnd = false;

    public Transform Target 
    { 
        get => target; 
        set => target = value; 
    }

    protected void Awake()
    {
        ChangeState(new IdleState());
    }


    protected override void Update()
    {
        base.Update();

        currentState.UpdateState();
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null) 
        {
            currentState.ExitState();
        }

        currentState = newState;

        currentState.EnterState(this);
    }
}
