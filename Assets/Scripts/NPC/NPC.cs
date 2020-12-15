using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    

    public virtual void DeSelectTarget()
    {

    }

    public virtual Transform SelectTarget()
    {
        return hitBox;
    }
}
