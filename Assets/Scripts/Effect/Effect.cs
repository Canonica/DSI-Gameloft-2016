using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour
{
    public string _name;
    public float _cost;
    public float _value;

    public virtual float Apply()
    {
        return _value;
    }
}
