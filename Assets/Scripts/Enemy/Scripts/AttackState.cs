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
        

        if (parent.AttackTime>=attackCoolDown && !parent.IsAttacking && parent.Target.GetComponent<Player>().IsAlive)
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

        if (parent.transform.tag != "Dragon")
        {
            parent.MyAnimator.SetTrigger("attack");

            Player p = parent.Target.GetComponent<Player>();

            p.TakeDamage(parent.EnemyAttackDmg, parent.transform);

            yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(2).length);
        }
        
        else 
        {
            EnemyAttackMovement e = parent.EnemyAttackPrefab.GetComponent<EnemyAttackMovement>();


            Debug.Log("Dragon Attack");

            e.InitTarget(parent.Target, parent.EnemyAttackDmg, parent.transform);
        }

        yield return new WaitForSeconds(0.5f);

        parent.IsAttacking = false;
    }
}
