using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : IEnemyState
{
    private Enemy parent;

    public void EnterState(Enemy parent)
    {
        this.parent = parent;
    }

    public void ExitState()
    {
        parent.Direction = Vector2.zero;
        parent.ResetEnemy();
    }

    public void UpdateState()
    {
        if (parent.Target != null && parent.InRange)
        {
            parent.ChangeState(new ChaseState());
        }

        else
        {
            parent.Direction = (parent.StartPos - parent.transform.position).normalized;

            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.StartPos, parent.Speed * Time.deltaTime);

            float distance = Vector2.Distance(parent.StartPos, parent.transform.position);

            if (distance <= 0)
            {
                parent.ChangeState(new IdleState());
            }
        }

        

        
    }
}
