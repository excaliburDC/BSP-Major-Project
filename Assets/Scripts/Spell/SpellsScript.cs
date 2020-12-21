using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsScript : MonoBehaviour
{

    private Rigidbody2D MyRigidBody;
    [SerializeField]
    private float speed;
    
    public Transform Target
    {
        get;
        private set;
    }

    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();

        
        
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //Vector2 direction = Target.position - transform.position;
        //MyRigidBody.velocity = direction.normalized * speed;
        ////to turn the fire ball
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void InitTarget(Transform target,int damage)
    {
        this.Target = target;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "HitBox" && col.transform==Target)
        {
            Invoke("DestroyEffect", 1.2f);
            col.GetComponentInParent<Enemy>().TakeDamage(damage);
           // col.attachedRigidbody.velocity = Vector2.zero;
            Target = null;
        }

        
    }

    void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
