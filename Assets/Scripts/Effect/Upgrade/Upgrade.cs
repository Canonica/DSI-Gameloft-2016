using UnityEngine;

public class Upgrade : Effect
{
    Motherbase _mb;
    public int _level;

    void Start()
    {
        _mb = GetComponent<Motherbase>();
        _level = 0;
    }

    public virtual void LevelOne(Unit unit)
    {
        UnitRush unitRush = (UnitRush)unit;
        unitRush.lifeSteal = true;
    }

    public virtual void LevelTwo(Unit unit)
    {
        UnitRush unitRush = (UnitRush)unit;
        unitRush.rangedAttack = true;
    }

    public virtual void LevelThree(int index)
    {
        if(index < _mb.maxNbOfUnits.Length)
        {
            _mb.maxNbOfUnits[index] += 5;
        }
    }

    public void LevelUp()
    {
        if (_level < 3)
        {
            _level++;
        }
        if (_level > 2)
        {
            LevelThree(_mb.upgrades.IndexOf(this));
        }
    }

    public void Use(Unit unit)
    {
        if(_level > 0)
        {
            LevelOne(unit);
        }
        if (_level > 1)
        {
            LevelTwo(unit);
        }
    }

}