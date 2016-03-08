using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class GoliathusUpgrade : Upgrade {

    public override void LevelOne(Unit unit)
    {
        base.LevelOne(unit);
        UnitTank tankUnit = (UnitTank)unit;
        if (tankUnit)
        {
            tankUnit.poison = true;
        }
    }

    public override void LevelTwo(Unit unit)
    {
        base.LevelTwo(unit);
        UnitTank tankUnit = (UnitTank)unit;
        if (tankUnit)
        {
            tankUnit.reflectDamage = true;
        }
    }

    public override void LevelThree(Unit unit)
    {
        base.LevelThree(unit);
        UnitTank tankUnit = (UnitTank)unit;
        if (tankUnit)
        {
            tankUnit.negateDamage = true;
        }
    }
}
