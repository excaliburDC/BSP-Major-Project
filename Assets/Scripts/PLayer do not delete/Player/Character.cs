using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;

    protected Rigidbody2D myRB;

    private Transform target;

    private Vector2 direction;

    public Animator  MyAnimator
    {
        get;
        set;
    }

    

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
        get;
        set;
    }
    

    protected virtual void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        //if (MyAnimator != null)
        //{
            MyAnimator = GetComponent<Animator>();
        //}

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
        myRB.velocity = Direction.normalized * Speed;
    }

    protected virtual void HandleLayers()
    {       
        if (IsMoving)
        {
            ActivateLayer("WalkLayer");
            MyAnimator.SetFloat("X", Direction.x);
            MyAnimator.SetFloat("Y", Direction.y);
            StopAttack();
        }
        else if(IsAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            ActivateLayer("IdleLayer");        
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
}
