using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName ="HealthPortion",menuName ="Item/Portion",order =1)]
public class HealthPortion : Item, IUseable
{
     
    [SerializeField]
    private int health;
    [SerializeField]
    private int typehealth;
    public void Use()
    {
        if(typehealth == 1)//player magic
        {
            if (Player.MyInstance.MyManaBar.MyCurrentValue < Player.MyInstance.MyManaBar.MyMaxValue)
            {
                Remove();
                Player.MyInstance.MyManaBar.MyCurrentValue += health;
            }
        }
        else if(typehealth == 2)//player blood

        {
            if (Player.MyInstance.MyHealthBar.MyCurrentValue < Player.MyInstance.MyHealthBar.MyMaxValue)
            {
                Remove();
                Player.MyInstance.MyHealthBar.MyCurrentValue += health;
            }
        }
        
    }
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n Use:Restores {0} health", health);
    }
}
