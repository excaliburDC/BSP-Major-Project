using System.Collections;
using UnityEngine;

public class AttackState : IEnemyState
{
    private Enemy parent;

    private float attackCoolDown = 3f;

    public void EnterState(Enemy parent)
    {
        this.parent = parent;
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
       

        if (parent.AttackTime>=attackCoolDown && !parent.IsAttacking)
        {
            parent.AttackTime = 0;

            parent.StartCoroutine(Attack());
        }

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

    public IEnumerator Attack()
    {
        parent.IsAttacking = true;

        // parent.MyAnimator.SetTrigger("attack");



        EnemyAttackMovement e = parent.EnemyAttackPrefab.GetComponent<EnemyAttackMovement>();

        e.InitTarget(parent.Target, parent.EnemyAttackDmg, parent.transform);


        yield return new WaitForSeconds(0.5f);

        parent.IsAttacking = false;
    }
}
