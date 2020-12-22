using UnityEngine;



public class Enemy : NPC
{

    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float initAggroRange;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private CanvasGroup healthGroup;
    [SerializeField]
    private GameObject enemyAttackPrefab;


    private IEnemyState currentState;
    
    

    private bool reachedPathEnd = false;

    public Vector3 StartPos
    {
        get;
        set;
    }

    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }

    public float AttackTime 
    { 
        get => attackTime; 
        set => attackTime = value; 
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

    public GameObject EnemyAttackPrefab 
    { 
        get => enemyAttackPrefab; 
        private set => enemyAttackPrefab = value; 
    }

    protected void Awake()
    {
        StartPos = transform.position;
        AggroRange = initAggroRange;
        ChangeState(new IdleState());
    }


    protected override void Update()
    {
        if(IsAlive)
        {
            if (!IsAttacking)
                AttackTime += Time.deltaTime;

            currentState.UpdateState();

            
        }

        base.Update();

     

    }

 
    public override Transform SelectTarget()
    {
        healthGroup.alpha = 1;

        return base.SelectTarget();
    }

    public override void DeSelectTarget()
    {
        healthGroup.alpha = 0;

        base.DeSelectTarget();
    }

    public override void TakeDamage(float damage, Transform source)
    {
        if(!(currentState is EvadeState))
        {
            SetTarget(source);

            base.TakeDamage(damage, source);
        }

        
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
        if(Target == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);

            AggroRange = initAggroRange;
            AggroRange += distance;
            Target = target;
        }
    }

    public void ResetEnemy()
    {
        this.Target = null;
        this.AggroRange = initAggroRange;
        this.health.MyCurrentValue = this.health.MyMaxValue;

        //reset health values
    }
}
