using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = " Create Weapon/Frost Weapon")]
public class FrostWeapon : Weapon
{
    public override void Attack()
    {
        Debug.Log("This deals ice damage");
        Debug.Log("Attack Power" + damage);
    }
}
