using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public Item typeCollect;
    public int bagSize;
    private bool IsAdded = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if (Input.GetMouseButtonDown(1))
            //{
            if (this.gameObject.name == "Bag")
            {
                Bag bag = (Bag)Instantiate(typeCollect);
                bag.Initalize(bagSize);
                InventoryScript.MyInstance.AddItem(bag);
                this.gameObject.SetActive(false);
            }
            else
            {     
                HealthPortion portion = (HealthPortion)Instantiate(typeCollect);
                InventoryScript.MyInstance.AddItem(portion);
                this.gameObject.SetActive(false);
            }
               // IsAdded = true;
            //}
        }
    }
    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (IsAdded)
    //    {
    //        this.gameObject.SetActive(false);
    //    }
    //}
}
