using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LayerSorter : MonoBehaviour
{
    
    private SpriteRenderer parentRenderer;

    private List<Obstracle> obstracle = new List<Obstracle>();
    void Start()
    {
        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();

    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag =="Obstracle")
        {
            Obstracle o = target.GetComponent<Obstracle>();
            o.FadeOut();
            obstracle.Add(o);
            if (obstracle.Count == 0 || (o.MySpriteRender.sortingOrder - 1 < parentRenderer.sortingOrder))
            {
                // transform.parent.GetComponent<SpriteRenderer>().sortingOrder= collision.GetComponent<SpriteRenderer>().sortingOrder - 1;
                parentRenderer.sortingOrder = o.MySpriteRender.sortingOrder - 1;
            }
            obstracle.Add(o);
        }
      
        
               
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstracle")
        {
            Obstracle o = collision.GetComponent<Obstracle>();
            o.FadeIn();
            obstracle.Remove(o);

            if(obstracle.Count==0)
            {
                parentRenderer.sortingOrder = 200;
            }else
            {
                obstracle.Sort();
                parentRenderer.sortingOrder = obstracle[0].MySpriteRender.sortingOrder - 1;
            }
            
        }
    }
    
}
