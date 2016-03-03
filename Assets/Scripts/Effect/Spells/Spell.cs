using UnityEngine;
using System.Collections;

public class Spell : Effect
{
    public enum SpellType
    {
        //Mettre les catégories d'Upgrade ici
        TestSpell,
        TestSpell2,
        TestSpell4,
        END,
    }

    public SpellType _type;

    public Spell(string parName, float parCost, float parValue, SpellType parType) : base(parName, parCost, parValue)
    {
        _type = parType;
    }

    public override float Apply()
    {
        Debug.Log("Spell : " + _name + " /// Cooldown : " + _cost);
        return base.Apply();
    }
}
