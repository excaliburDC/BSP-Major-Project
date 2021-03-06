﻿
using UnityEngine;

public class ChaseState : IEnemyState
{
    private Enemy parent;

    public void EnterState(Enemy parent)
    {
        this.parent = parent;
    }

    public void ExitState()
    {
        parent.Direction = Vector2.zero;
    }

    public void UpdateState()
    {
       
        if (parent.Target!=null)
        {
            parent.Direction = (parent.Target.position - parent.transform.position).normalized;

            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.Target.position, parent.Speed * Time.deltaTime);

            float distance = Vector2.Distance(parent.Target.position, parent.transform.position);

            if(distance <= parent.AttackRange)
            {
                parent.ChangeState(new AttackState());
            }
        }

        if (!parent.InRange)
        {
            parent.ChangeState(new EvadeState());
        }

        //else
        //{
        //    parent.ChangeState(new IdleState());
        //}
        
    }
}
