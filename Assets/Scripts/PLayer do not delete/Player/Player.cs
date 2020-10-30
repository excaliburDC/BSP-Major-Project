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

    protected override void Start()
    {
        MyHealthBar.Initialize(maxHealth, maxHealth);
        Bar2.Initialize(maxThirst, maxThirst);
        base.Start();      
    }
    protected override void Update()
    {
        GetInput();

        
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
        if (Input.GetKey(KeyCode.W))
        {
            Direction += Vector2.up;           
        }
        if (Input.GetKey(KeyCode.A))
        {
            Direction += Vector2.left;           
        }
        if (Input.GetKey(KeyCode.S))
        {
            Direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Direction += Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attackCoroutine = StartCoroutine(Attack());

        }
    }

    //Attack Animations
    private IEnumerator Attack()
    {
        if (!IsAttacking && !IsMoving)
        {
            IsAttacking = true;
            MyAnimator.SetBool("attack", IsAttacking);



            //for testing purposes
            Weapon currentWeapon = (Weapon) playerWeaponsList[0].GetComponent<Collectables>().typeCollect;
            currentWeapon.Attack();

            yield return new WaitForSeconds(0.7f);
            Debug.Log("done attacking");
            StopAttack();
        }
    }
   
}
