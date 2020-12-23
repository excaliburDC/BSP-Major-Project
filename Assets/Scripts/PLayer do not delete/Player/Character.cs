using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;

    protected Rigidbody2D myRB;

    private Transform target;

    private Vector2 direction;

    [SerializeField]
    protected Stats health;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    protected Transform hitBox;


    public Animator  MyAnimator
    {
        get;
        set;
    }

    public Stats MyHealthBar { get => health; set => health = value; }

    //protected bool IsAttacking;
    protected Coroutine attackCoroutine;

    public bool IsMoving
    {
        get
        {
            return (Direction.x != 0 || Direction.y != 0);
        }
       
    }

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    public Vector2 Direction 
    { 
        get => direction; 
        set => direction = value; 
    }
    public float Speed 
    { 
        get => speed; 
        set => speed = value; 
    }

    public bool IsAttacking
    {
        get;
        set;
    }

    //To be done after adding health system
    public bool IsAlive
    {
        get 
        {
            return health.MyCurrentValue > 0;
        }
       
    }
    

    protected virtual void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        
        MyAnimator = GetComponent<Animator>();

        MyHealthBar.Initialize(maxHealth, maxHealth);

    }
    protected virtual void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        //if (MyAnimator != null)
        //{
            HandleLayers();
        //}

    }
    protected virtual void FixedUpdate()
    {

        Move();
    }
    public void Move()
    {
        if(IsAlive)
        {
            myRB.velocity = Direction.normalized * Speed;
        }
        
    }

    protected virtual void HandleLayers()
    {
        if(IsAlive)
        {
            if (IsMoving)
            {
                ActivateLayer("WalkLayer");
                MyAnimator.SetFloat("X", Direction.x);
                MyAnimator.SetFloat("Y", Direction.y);
                StopAttack();
            }
            else if (IsAttacking)
            {
                ActivateLayer("AttackLayer");
            }
            else
            {
                ActivateLayer("IdleLayer");
            }
        }

        else
        {
            ActivateLayer("DeathLayer");
        }

              
    }
   
    private void ActivateLayer(string layerName)
    {
        for(int i=0;i< MyAnimator.layerCount;i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    public void StopAttack()
    {
        if(attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            IsAttacking = false;
            MyAnimator.SetBool("attack", IsAttacking);
            
        }
       
    }

    public virtual void TakeDamage(float damage,Transform source)
    {
        
        health.MyCurrentValue -= damage;
    

        if (health.MyCurrentValue <= 0) 
        {
            Direction = Vector2.zero;
            myRB.velocity = direction;
            //die
            MyAnimator.SetTrigger("die");
        }
    }
}
