using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;


public class Enemy : NPC
{

    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float initAggroRange;
    
    
    private IEnemyState currentState;

    

    private bool reachedPathEnd = false;

    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }

    public float AggroRange
    {
        get;
        set;
    }

    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, Target.position) < AggroRange;
        }
    }


    protected void Awake()
    {
        AggroRange = initAggroRange;
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

    public void SetTarget(Transform target)
    {
        if(Target == null)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            AggroRange = initAggroRange;
            AggroRange += distance;
            Target = target;
        }
    }
}
