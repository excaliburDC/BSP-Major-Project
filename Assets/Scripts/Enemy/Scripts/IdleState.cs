using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy parent;

    public void EnterState(Enemy parent)
    {
        this.parent = parent;
        
       // this.parent.ResetEnemy();
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        //change to chase state

        Debug.Log("Idle");

        if (parent.Target != null) 
        {
            parent.ChangeState(new ChaseState());
        }
    }
}
