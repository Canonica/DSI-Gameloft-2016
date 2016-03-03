using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour
{
    public string _name;
    public float _cost;
    public float _value;

    public Effect(string parName, float parCost, float parValue)
    {
        _name = parName;
        _cost = parCost;
        _value = parValue;
    }

    public virtual float Apply()
    {
        return _value;
    }
}
