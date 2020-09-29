using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D myRB;

    protected Vector2 direction;

    protected Animator  MyAnimator;

    protected bool IsAttacking;

    protected Coroutine attackCoroutine;
    public bool IsMoving
    {
        get
        {
            return (direction.x != 0 || direction.y != 0);
        }
       
    }


    protected virtual void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        //transform.Translate(direction * speed * Time.deltaTime);
        HandleLayers();
    }
    private void FixedUpdate()
    {

        Move();
    }
    public void Move()
    {
        myRB.velocity = direction.normalized * speed;
    }

    protected virtual void HandleLayers()
    {       
        if (IsMoving)
        {
            ActivateLayer("WalkLayer");
            MyAnimator.SetFloat("X", direction.x);
            MyAnimator.SetFloat("Y", direction.y);
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
