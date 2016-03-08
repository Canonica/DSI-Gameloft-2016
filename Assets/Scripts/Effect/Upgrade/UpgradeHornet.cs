using UnityEngine;

public class UpgradeHornet : Upgrade
{
    Motherbase _mb;
    public int _level;

    void Start()
    {
        _mb = GetComponent<Motherbase>();
        _level = 0;
    }

    public override void LevelOne(Unit unit)
    {
        UnitJump unitJump = (UnitJump)unit;
        unitJump.hasUpgrade1 = true;
    }

    public override void LevelTwo(Unit unit)
    {
        UnitJump unitJump = (UnitJump)unit;
        unitJump.hasUpgrade2 = true;
    }

    public override void LevelThree(int index)
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