using UnityEngine;

public class Upgrade : Effect
{
    public enum UpgradeType
    {
        //Mettre les catégories d'Upgrade ici
        TestUpgrade,
        TestUpgrade2,
        TestUpgrade4,
        END,
    }
    
    public UpgradeType _type;
    public float _level;
    public float _addByLevel;

    public Upgrade(string parName, float parCost, float parValue, UpgradeType parType , float parAddByLevel) : base(parName, parCost, parValue)
    {
        _level = 0;
        _addByLevel = parAddByLevel;
        _type = parType;
    }

    public override float Apply()
    {
        Debug.Log("Upgrade : " + _name + " /// Cost : " + _cost);
        return base.Apply() * (_level);
    }

    public void LevelUp()
    {
        _level += _addByLevel;
    }

}
