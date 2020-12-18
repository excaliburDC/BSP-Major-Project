using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private NPC currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickTarget();
    }
            
    private void ClickTarget()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity,1024 );
            Debug.Log("Mouse Click");
            if(hit.collider != null)
            {
                //if (hit.collider.tag == "Enemy")
                //{
                // player.MyTarget = hit.transform;
                //}

                Debug.Log(hit.collider.name);

                if(currentTarget!=null)
                {
                    currentTarget.DeSelectTarget();
                }

                currentTarget = hit.collider.GetComponent<NPC>();

                player.MyTarget = currentTarget.SelectTarget();
            }
            else
            {
                if(currentTarget!=null)
                {
                    currentTarget.DeSelectTarget();
                }

                currentTarget = null;
                //it detargets the enemy
                player.MyTarget = null;
            }
        }
    }
}
