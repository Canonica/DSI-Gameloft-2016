using UnityEngine;
using System.Collections;

public class CockroachUpgrade : Upgrade {

    public bool hasUpgradedCockroach = false;
    public int numberCRUpgrade = 1;

    /*
       Increases the number of cockroach spawned (to 4/5)
       When a cockroach dies, it heals a nearby cockroach
       All new cockroaches are now improved, with increased stats and new visuals (berserk)
   */

    public override void LevelTwo(Unit unit)
    {
        base.LevelTwo(unit);
        BaseUnit baseUnit = (BaseUnit)unit;
        if(baseUnit)
        {
            if(!hasUpgradedCockroach)
            {
                hasUpgradedCockroach = true;
                _mb.units[0].GetComponent<Unit>().groupSpawn += numberCRUpgrade;
            }
        }
    }

    public override void LevelOne(Unit unit)
    {
        base.LevelOne(unit);
        BaseUnit baseUnit = (BaseUnit)unit;
        if (baseUnit)
        {
            baseUnit.isRegeneratingCR = true;
        }
    }

    public override void LevelThree(Unit unit)
    {
        base.LevelThree(unit);
        BaseUnit baseUnit = (BaseUnit)unit;
        if (baseUnit)
        {
            baseUnit.Berzerker();
        }
    }

    void OnDestroy()
    {
        if (hasUpgradedCockroach)
        {
            _mb.units[0].GetComponent<Unit>().groupSpawn -= numberCRUpgrade;
        }
    }
}
