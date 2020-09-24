﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Bag",menuName ="Item/Bag",order =1)]
public class Bag : Item, IUseable
{
    private int slots;

    [SerializeField]
    private GameObject bagPrefab;
    [SerializeField]
    private Sprite icon;
    public BagScript MyBagScript{get; set;}

    public BagButton MyBagButton { get; set; }
    public int Slots
    {
        get
        {
            return slots;
        }
    }

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public void Initalize(int slots)
    {
        this.slots = slots;
    }

    public void Use()
    {
        if (InventoryScript.MyInstance.CanAddBag)
        {
            Remove();
            MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlot(slots);
            if (MyBagButton == null)
            {
                InventoryScript.MyInstance.AddBag(this);
            }else
            {
                InventoryScript.MyInstance.AddBag(this,MyBagButton);
            }
        }
    }
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n{0} slotbag", slots);
    }
}
