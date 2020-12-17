using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{

    private Rigidbody2D MyRigidBody;
    [SerializeField]
    private float speed;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Dragon").transform;
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 direction = target.position - transform.position;
        MyRigidBody.velocity = direction.normalized * speed;
        //to turn the fire ball
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
