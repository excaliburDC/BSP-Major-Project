
using UnityEngine;

public class AttackState : IEnemyState
{
    private Enemy parent;

    public void EnterState(Enemy parent)
    {
        this.parent = parent;
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        Debug.Log("Attacking");

        if (parent.Target != null)
        {
            

            //Attack from different sets of attacks

            float distance = Vector2.Distance(parent.Target.position, parent.transform.position);

            if (distance >= parent.AttackRange)
            {
                parent.ChangeState(new ChaseState());
            }
        }

        else
        {
            parent.ChangeState(new IdleState());
        }
    }
}
