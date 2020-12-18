using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    
    [SerializeField]
    private Stats Bar2;
   
    [SerializeField]
    private float maxThirst;

   
    private static Player instance;
    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    
    public Transform MyTarget { get; set; }

    [SerializeField]
    private GameObject[] spellPrefab;
    [SerializeField]
    private Transform[] ExitPoints;
    [SerializeField]
    private Blocks[] blocks;
    private int ExitIndex;

    protected override void Start()
    {
        Bar2.Initialize(maxThirst, maxThirst);
        base.Start();      
    }
    protected override void Update()
    {
        GetInput();
        //Debug.Log(LayerMask.GetMask("Block"));
       // InLineOfSight();
        base.Update(); 
        

    }
  
    private void  GetInput()
    {
        Direction = Vector2.zero;


        //this is for health bar
        if (Input.GetKeyDown(KeyCode.O))
        {
            MyHealthBar.MyCurrentValue -= 10;
            Bar2.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            MyHealthBar.MyCurrentValue += 10;
            Bar2.MyCurrentValue += 10;
        }
        if (Input.GetKey(KeyCode.W))//up
        {
            ExitIndex = 0;
            Direction += Vector2.up;           
        }
        if (Input.GetKey(KeyCode.A))//left
        {
            ExitIndex = 2;
            Direction += Vector2.left;           
        }
        if (Input.GetKey(KeyCode.S))//down
        {
            ExitIndex = 1;
            Direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))//right
        {
            ExitIndex = 3;
            Direction += Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    //Attack Animations
    private IEnumerator Attack(int spellIndex)
    {
     
        IsAttacking = true;
        MyAnimator.SetBool("attack", IsAttacking);



        //for testing purposes
        //Weapon currentWeapon = (Weapon) playerWeaponsList[0].GetComponent<Collectables>().typeCollect;
        //currentWeapon.Attack();

        yield return new WaitForSeconds(0.7f);

        //CastSpell();

        //if(spellPrefab[0])
        Spells s = Instantiate(spellPrefab[spellIndex], ExitPoints[ExitIndex].position, Quaternion.identity).GetComponent<Spells>();

        s.Target = MyTarget;

        //Debug.Log("done attacking");
        StopAttack();
    }

    public void CastSpell(int spellIndex)
    {
        Block();
        if (MyTarget != null && !IsAttacking && !IsMoving && InLineOfSight())
        {
            attackCoroutine = StartCoroutine(Attack(spellIndex));
        }

        
    
    }
    private bool InLineOfSight()
    {
        if(MyTarget!=null)
        {
            Vector2 targetdirection = (MyTarget.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetdirection, Vector2.Distance(transform.position, MyTarget.transform.position), 512);
            
            //if we didn't hit the block, we cast a spell
            if (hit.collider == null)
            {
                return true;
            }
            //Debug.DrawRay(transform.position, targetdirection, Color.red);
           
        }

        //if we hit the block, we can cast a spell
        return false;

    }
    public void Block()
    {
        foreach(Blocks b in blocks)
        {
            b.Deactivate();
        }
        blocks[ExitIndex].Activate();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>());
        }
    }

}
