using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = " Create Weapon/Fire Weapon")]
public class FireWeapon : Weapon
{
    public override void Attack()
    {
        Debug.Log("This deals fire damage");
        Debug.Log("Attack Power" + damage);
    }
}
