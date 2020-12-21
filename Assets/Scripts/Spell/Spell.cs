using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell 
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int damage;
    [SerializeField]
    private GameObject spellPrefab;

    public string Name { get => name;  }
    public int Damage { get => damage;  }
    public GameObject SpellPrefab { get => spellPrefab;  }
}
