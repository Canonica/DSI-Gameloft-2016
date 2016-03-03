using UnityEngine;
using System.Collections;

public class Motherbase : Entity
{


    [Header("Spawner Option")]
    public GameObject[] units;
    public float delay;
    public Waypoint waypoint;
    public GameObject targetBase;
    bool spawning;
    int typeOfUnit;

    int setNb;

    // Use this for initialization
    void Awake()
    {
        _life = 10;
        spawning = false;
    }


    // Update is called once per frame


    public override void Start()
    {
        base.Start();
        units[0].GetComponent<Unit>()._currentNbOfUnit = 0;
        units[1].GetComponent<Unit>()._currentNbOfUnit = 0;
        //units[2].GetComponent<Unit>()._currentNbOfUnit = 0;
        //units[3].GetComponent<Unit>()._currentNbOfUnit = 0;
    }

    public override void Update()
    {
        base.Update();
        Debug.Log(units[0].GetComponent<Unit>()._currentNbOfUnit);
        //if (Input.GetButtonDown("RB_button_" + _playerId))
        //{
        //    if(setNb>(units.Length)/4)
        //    {
        //        setNb--;
        //    }
        //    else
        //    {
        //        setNb++;
        //    }
        //}
        //if (Input.GetButtonDown("LB_button_" + _playerId))
        //{
        //    if (setNb > 0)
        //    {
        //        setNb--;
        //    }
        //}
        if (GameManager.instance.currentGamestate == GameManager.gameState.Playing && !spawning)
        {
            spawning = true;
            StartCoroutine(loadUnit1());
            StartCoroutine(loadUnit2());
            //StartCoroutine(loadUnit3());
            //StartCoroutine(loadUnit4());
            Debug.Log(GameManager.instance.currentGamestate);
            if (Input.GetButtonDown("RB_button_" + _playerId))
            {
                if (setNb > (units.Length) / 4)
                {
                    setNb--;
                }
                else
                {
                    setNb++;
                }
            }
            if (Input.GetButtonDown("LB_button_" + _playerId))
            {
                if (setNb > 0)
                {
                    setNb--;
                }
            }
            Debug.Log(typeOfUnit);
            if (GameManager.instance.currentGamestate == GameManager.gameState.Playing && Input.GetButtonDown("Fire " + _playerId))
            {
                Spawner();
            }

            if (Input.GetButtonDown("Fire " + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
            {
                typeOfUnit = 0;
                corSpawnUnits(typeOfUnit);
            }

            if (Input.GetButtonDown("B_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
            {
                typeOfUnit = 1;
                corSpawnUnits(typeOfUnit);
            }

            if (Input.GetButtonDown("X_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
            {
                typeOfUnit = 2;
                corSpawnUnits(typeOfUnit);
            }

            if (Input.GetButtonDown("Y_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
            {
                typeOfUnit = 3;
                corSpawnUnits(typeOfUnit);
            }

            // DEBUG
            if (Input.GetKeyDown(KeyCode.S))
            {
                corSpawnUnits(0);
            }

        }
    }
    //IEnumerator Spawner()
    //{
    //    while (_life > 0)
    //    {
    //        corSpawnUnits(typeOfUnit);
    //        yield return new WaitForSeconds(units[typeOfUnit].GetComponent<Unit>()._hatchTime);
    //    }
    //}
    void Spawner()
    {
        int nb = units[typeOfUnit].GetComponent<Unit>().groupSpawn;
        if (nb > 1)
        {
            groupSpawner(typeOfUnit, nb);
        }
        else
        {
            corSpawnUnits(typeOfUnit);
        }
    }

    //void spawnUnits(int index)
    //{
    //    GameObject obj = Instantiate(units[index], transform.position, transform.rotation) as GameObject;
    //    obj.GetComponent<Unit>()._playerId = idPlayer;
    //    obj.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].transform.position);
    //    obj.GetComponent<Unit>()._enemyMotherBase = targetBase;
    //    obj.GetComponent<Unit>().waypointDest = waypoints[2].transform.position;
    //    obj.transform.parent = transform;
    //}

    public void getDamage(int dmg)
    {
        if (dmg > _life)
        {
            _life = 0;
            EndGameManager.instance.motherBaseDead(_playerId);
        }
        else
        {
            if (dmg > 0)
                _life -= dmg;
        }
    }

    void corSpawnUnits(int typeOfUnit)
    {
        Debug.Log(units[typeOfUnit].GetComponent<Unit>()._currentNbOfUnit);
        if(units[typeOfUnit].GetComponent<Unit>()._currentNbOfUnit > 0) { 
            GameObject prefabOfUnit = Instantiate(units[typeOfUnit], transform.position, transform.rotation) as GameObject;
            Unit unit = prefabOfUnit.GetComponent<Unit>();
            NavMeshAgent nav = prefabOfUnit.GetComponent<NavMeshAgent>();
            unit._playerId = _playerId;
            nav.SetDestination(waypoint.pos);
            unit._enemyMotherBase = targetBase;
            unit.waypointDest = waypoint;
            prefabOfUnit.transform.parent = transform;
            units[typeOfUnit].GetComponent<Unit>()._currentNbOfUnit--;
        }
    }

    IEnumerator loadUnit1()
    {
        while(_life > 0)
        {
            Unit unit = units[0].GetComponent<Unit>();
            if (unit._currentNbOfUnit < unit._maxNbOfUnit)
            {
                unit._currentNbOfUnit++;
            }
            yield return new WaitForSeconds(unit._hatchTime);
        }
        
        
    }

    IEnumerator loadUnit2()
    {
        while (_life > 0)
        {
            Unit unit = units[1].GetComponent<Unit>();
            if (unit._currentNbOfUnit < unit._maxNbOfUnit)
            {
                unit._currentNbOfUnit++;
            }
            yield return new WaitForSeconds(unit._hatchTime);
        }
       
    }

    IEnumerator loadUnit3()
    {
        while (_life > 0)
        {
            Unit unit = units[2].GetComponent<Unit>();
            if (unit._currentNbOfUnit < unit._maxNbOfUnit)
            {
                unit._currentNbOfUnit++;
            }
            yield return new WaitForSeconds(unit._hatchTime);
        }
    }

    IEnumerator loadUnit4()
    {
        while (_life > 0)
        {
            Unit unit = units[3].GetComponent<Unit>();
            if (unit._currentNbOfUnit < unit._maxNbOfUnit)
            {
                unit._currentNbOfUnit++;
            }
            yield return new WaitForSeconds(unit._hatchTime);
        }        
        GameObject prefabOfUnit = Instantiate(units[typeOfUnit], transform.position, transform.rotation) as GameObject;
        Unit unit2 = prefabOfUnit.GetComponent<Unit>();
        NavMeshAgent nav = prefabOfUnit.GetComponent<NavMeshAgent>();
        unit2._playerId = _playerId;
        nav.SetDestination(waypoint.pos);
        unit2._enemyMotherBase = targetBase;
        unit2.waypointDest = waypoint;
        prefabOfUnit.transform.parent = transform;
    }

    void groupSpawner(int typeOfUnit, int nb)
    {
        for (int i = 0; i <= nb; i++)
        {
            corSpawnUnits(typeOfUnit);
        }
    }
}
