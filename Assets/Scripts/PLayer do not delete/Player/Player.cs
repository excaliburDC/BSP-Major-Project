using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stats Bar1;
    [SerializeField]
    private Stats Bar2;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxThirst;

    //for testing purposes
    [SerializeField]
    private List<GameObject> playerWeaponsList;
   
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

    public Stats MyHealthBar { get => Bar1; set => Bar1 = value; }
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
        
        MyHealthBar.Initialize(maxHealth, maxHealth);
        Bar2.Initialize(maxThirst, maxThirst);
        base.Start();      
    }
    protected override void Update()
    {
        GetInput();
        //Debug.Log(LayerMask.GetMask("Block"));
        //InLineOfSight();
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
            ExitIndex = 3;
            Direction += Vector2.left;           
        }
        if (Input.GetKey(KeyCode.S))//down
        {
            ExitIndex = 2;
            Direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))//right
        {
            ExitIndex = 1;
            Direction += Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Block();
            if (MyTarget != null && !IsAttacking && !IsMoving && InLineOfSight())
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }
    }

    //Attack Animations
    private IEnumerator Attack()
    {
     
            IsAttacking = true;
            MyAnimator.SetBool("attack", IsAttacking);



            //for testing purposes
            //Weapon currentWeapon = (Weapon) playerWeaponsList[0].GetComponent<Collectables>().typeCollect;
            //currentWeapon.Attack();

            yield return new WaitForSeconds(0.7f);
            CastSpell();
            //Debug.Log("done attacking");
            StopAttack();
    }

    public void CastSpell()
    {
        Instantiate(spellPrefab[0], ExitPoints[ExitIndex].position, Quaternion.identity);
    
    }
    private bool InLineOfSight()
    {
        Vector2 targetdirection = (MyTarget.position - transform.position) ;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetdirection, Vector2.Distance(transform.position, MyTarget.transform.position),512);
        if(hit.collider == null)
        {
            return true;
        }
        //Debug.DrawRay(transform.position, targetdirection, Color.red);
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
