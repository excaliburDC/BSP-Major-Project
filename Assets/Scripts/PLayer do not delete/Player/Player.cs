using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    
    [SerializeField]
    private Stats mana;
   
    [SerializeField]
    private float maxMana;
    [SerializeField]
    private GameObject GameOverPopUp;
    public bool IsManaAvailable
    {
        get
        {
            return MyManaBar.MyCurrentValue > 0;
        }
    }

    
   
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

    public Stats MyManaBar { get => mana; set => mana = value; }

    [SerializeField]
    private Transform[] ExitPoints;
    [SerializeField]
    private Blocks[] blocks;
    private int ExitIndex;

    private SpellBook spellBook;

    protected override void Start()
    {
        spellBook = GetComponent<SpellBook>();
        MyManaBar.Initialize(maxMana, maxMana);
        base.Start();      
    }
    protected override void Update()
    {
        if(!IsAlive)
        {
            GameOverPopUp.SetActive(true);
        }
        GetInput();
        //Debug.Log(LayerMask.GetMask("Block"));
       // InLineOfSight();
        base.Update(); 
        
        if(!IsManaAvailable)
        {
            StartCoroutine(ResetMana());
        }

    }

    private IEnumerator ResetMana()
    {
        yield return new WaitForSeconds(3f);

        this.MyManaBar.MyCurrentValue += 20;

       
    }

    private void  GetInput()
    {
        Direction = Vector2.zero;


        //this is for health bar
        if (Input.GetKeyDown(KeyCode.O))
        {
            MyHealthBar.MyCurrentValue -= 10;
            MyManaBar.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            MyHealthBar.MyCurrentValue += 10;
            MyManaBar.MyCurrentValue += 10;
        }
        if(IsAlive)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))//up
            {
                ExitIndex = 0;
                Direction += Vector2.up;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))//left
            {
                ExitIndex = 2;
                Direction += Vector2.left;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))//down
            {
                ExitIndex = 1;
                Direction += Vector2.down;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))//right
            {
                ExitIndex = 3;
                Direction += Vector2.right;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }
       

        
    }

    //Attack Animations
    private IEnumerator Attack(int spellIndex)
    {
        Transform currentTarget = Target;

        Spell newSpell = spellBook.CastSpell(spellIndex);
     
        IsAttacking = true;
        MyAnimator.SetBool("attack", IsAttacking);

        MyManaBar.MyCurrentValue -= newSpell.ManaCost;


        //for testing purposes
        //Weapon currentWeapon = (Weapon) playerWeaponsList[0].GetComponent<Collectables>().typeCollect;
        //currentWeapon.Attack();

        yield return new WaitForSeconds(0.7f);

        //CastSpell();

        SpellsScript s;

        if(currentTarget!=null && InLineOfSight() && MyManaBar.MyCurrentValue > 10 && MyHealthBar.MyCurrentValue!=0)
        {
            if (spellIndex == 0)
            {
              
                MyManaBar.MyCurrentValue -= 10;
                s = Instantiate(newSpell.SpellPrefab, currentTarget.position, Quaternion.identity).GetComponent<SpellsScript>();
                s.InitTarget(currentTarget, newSpell.Damage,transform);
            }

            if (spellIndex == 1)
            {
                MyManaBar.MyCurrentValue -= 10;
                s = Instantiate(newSpell.SpellPrefab, currentTarget.position - new Vector3(0f, 0.2f, 0f), Quaternion.identity).GetComponent<SpellsScript>();
                s.InitTarget(currentTarget, newSpell.Damage,transform);
            }

            else if (spellIndex == 2)
            {
                MyManaBar.MyCurrentValue -= 10;
                s = Instantiate(newSpell.SpellPrefab, currentTarget.position - new Vector3(0f, -0.5f, 0f), Quaternion.identity).GetComponent<SpellsScript>();
                s.InitTarget(currentTarget, newSpell.Damage,transform);
            }
        }

        



        //Spells s = Instantiate(spellPrefab[spellIndex], ExitPoints[ExitIndex].position, Quaternion.identity).GetComponent<Spells>();

        // s.Target = MyTarget;


        //Debug.Log("done attacking");
        StopAttack();
    }

    public override void TakeDamage(float damage, Transform source)
    {
       
        base.TakeDamage(damage, source);
    }

    public void CastSpell(int spellIndex)
    {
        Block();

        if (Target != null && Target.GetComponentInParent<Character>().IsAlive && !IsAttacking && !IsMoving && InLineOfSight() && IsManaAvailable )
        {
            attackCoroutine = StartCoroutine(Attack(spellIndex));
        }

        
    
    }
    private bool InLineOfSight()
    {
        if(Target!=null)
        {
            Vector2 targetdirection = (Target.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetdirection, Vector2.Distance(transform.position, Target.transform.position), 512);
            
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
