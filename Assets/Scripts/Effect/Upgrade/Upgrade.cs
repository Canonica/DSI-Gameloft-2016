using UnityEngine;

public class Upgrade : Effect
{
    [HideInInspector]
    public Motherbase _mb;

    public int _levelMax = 3;
    public bool _levelOne;
    public bool _levelTwo;
    public bool _levelThree;


    void Start()
    {
        _mb = GetComponent<Motherbase>();
    }

    public virtual void LevelOne(Unit unit)
    {
        //unit._damage += 2;
    }

    public virtual void LevelTwo(Unit unit)
    {
        // unit._hatchTime -= 0.5f;
    }

    public virtual void LevelThree(Unit unit)
    {
        /*if(index < _mb.maxNbOfUnits.Length)
        {
            _mb.maxNbOfUnits[index] += 5;
        }*/
    }

    public int PreLevelUp()
    {
        int level = -1;
        if (!(_levelOne && _levelTwo && _levelThree))
        {
            level = Random.Range(0, _levelMax * 100);

            if ((level < 100 || (_levelTwo && _levelThree)) && !_levelOne)
            {
                level = 1;
            }
            else if ((level < 200 || _levelThree) && !_levelTwo)
            {
                level = 2;
            }
            else if (level < 300 && !_levelThree)
            {
                level = 3;
            }
        }
        return level;

    }

    public bool LevelUp(int level)
    {
        if (level == 1)
        {
            return _levelOne = true;
        }
        else if (level == 2)
        {
            return _levelTwo = true;
        }

        else if (level == 3)
        {
            return _levelThree = true;
        }

        return false;

    }

    public void Use(Unit unit)
    {
        if (_levelOne)
        {
            LevelOne(unit);
        }
        if (_levelTwo)
        {
            LevelTwo(unit);
        }
        if (_levelThree)
        {
            LevelThree(unit);
        }
    }

}