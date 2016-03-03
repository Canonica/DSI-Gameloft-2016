using UnityEngine;
using System.Collections;

public class Spell : Effect {

	public Spell(string parName, float parCost, float parValue) : base(parName, parCost, parValue)
    {

    }

    public override float Apply()
    {
        Debug.Log("Spell : " + _name + " /// Cooldown : " + _cost);
        return base.Apply();
    }
}
