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

    public bool LevelUp()
    {

        bool hasLevelUp = false;

        int level = Random.Range(0, _levelMax * 100);

        if ((level < 100 || (_levelTwo && _levelThree)) && !_levelOne)
        {
            _levelOne = true;
            hasLevelUp = true;
        }
        else if ((level < 200 || _levelThree) && !_levelTwo)
        {
            _levelTwo = true;
            hasLevelUp = true;
        }

        else if (level < 300 && !_levelThree)
        {
            _levelThree = true;
            hasLevelUp = true;
        }


        return hasLevelUp;

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