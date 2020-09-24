using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality {  Common ,Uncommon,Rare,Epic}
public abstract class Item : ScriptableObject,IMoveable,IDescribable
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    public string objType;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;

    private SlotScript slot;
  
    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }
    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public SlotScript MySlot {
        get => slot; set => slot = value;
    }

    public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }

    public virtual string GetDescription()
    {
        string color = string.Empty;
        switch(quality)
        {
            case Quality.Common:
                color = "#E97200";
                break;
            case Quality.Uncommon:
                color = "#00ff00ff";
                break;
            case Quality.Rare:
                color = "#0000ffff";
                break;
            case Quality.Epic:
                color = "#800080ff";
                break;
        }
        return string.Format("<color={0}>{1}</color>", color, title);
    }
}
