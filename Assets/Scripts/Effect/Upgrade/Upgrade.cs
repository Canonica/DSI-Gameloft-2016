using UnityEngine;

public class Upgrade : Effect
{
    public Upgrade(string parName, float parCost, float parValue) : base(parName, parCost, parValue)
    {
        
    }

    public override float Apply()
    {
        Debug.Log("Upgrade : " + _name + " /// Cost : " + _cost);
        return base.Apply();
    }

}
