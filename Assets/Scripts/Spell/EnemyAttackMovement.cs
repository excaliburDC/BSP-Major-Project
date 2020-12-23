using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMovement : MonoBehaviour
{
    private Rigidbody2D MyRigidBody;
    [SerializeField]
    private float speed;
    

    public Transform Target
    {
        get;
        set;
    }

    private Transform source;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(Target!=null)
        {
            Vector2 direction = Target.position - transform.position;
            MyRigidBody.velocity = direction.normalized * speed;

            //to turn the fire ball
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
        

    public void InitTarget(Transform target, int damage, Transform source)
    {
        this.Target = target;
        this.damage = damage;
        this.source = source;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && col.transform == Target)
        {
            speed = 0;

            Character c = col.GetComponentInParent<Player>();

            GetComponent<Animator>().SetTrigger("impact");

            c.TakeDamage(damage, source);

            MyRigidBody.velocity = Vector2.zero;

            Target = null;

            Destroy(gameObject,0.5f);
        }


    }

}
