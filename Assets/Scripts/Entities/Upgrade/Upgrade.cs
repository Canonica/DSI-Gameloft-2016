using UnityEngine;

public class Upgrade
{

    public string _name;
    public int _cost;
    public float _value;

    public Upgrade(string parName, int parCost, float parValue)
    {
        _name = parName;
        _cost = parCost;
        _value = parValue;
    }

    public virtual float Apply()
    {
        Debug.Log("Upgrade : " + _name + " /// Cost : " + _cost);
        return default(float);
    }

}
