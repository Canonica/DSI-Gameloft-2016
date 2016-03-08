using UnityEngine;
using System.Collections;

public class Spell : Effect
{

    void Start()
    {

    }

    public override float Apply()
    {
        Debug.Log("Spell : " + _name + " /// Cooldown : " + _cost);
        return base.Apply();
    }
}
