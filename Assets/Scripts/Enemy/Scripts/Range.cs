using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private Enemy parentObj;

    private void Start()
    {
        parentObj = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Entering enemy Range");
            parentObj.Target = col.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Exiting enemy Range");
            parentObj.Target = null;
        }
    }
}
