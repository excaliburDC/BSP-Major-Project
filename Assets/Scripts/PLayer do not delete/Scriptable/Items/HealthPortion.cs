using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName ="HealthPortion",menuName ="Item/Portion",order =1)]
public class HealthPortion : Item, IUseable
{
     
    [SerializeField]
    private int health;
    
    public void Use()
    {
        if (Player.MyInstance.MyHealthBar.MyCurrentValue < Player.MyInstance.MyHealthBar.MyMaxValue)
        {
            Remove();
            Player.MyInstance.MyHealthBar.MyCurrentValue += health;
        }
    }
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n Use:Restores {0} health", health);
    }
}
