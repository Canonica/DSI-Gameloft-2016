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
        typeOfUnit = 0;
    }

    public override void Update()
    {
        base.Update();
        Debug.Log(GameManager.instance.currentGamestate);
        if (Input.GetButtonDown("RB_button_" + _playerId))
        {
            if(setNb>(units.Length)/4)
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
        if (GameManager.instance.currentGamestate == GameManager.gameState.Playing && Input.GetButtonDown("Fire "+_playerId))
        {
            Spawner();
        }

        if (Input.GetButtonDown("Fire " + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing )
        {
            Debug.Log("toto");
            typeOfUnit = 0;
        }

        if (Input.GetButtonDown("B_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing )
        {
            typeOfUnit = 1;
        }

        if (Input.GetButtonDown("X_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
        {
            typeOfUnit = 2;
        }

        if (Input.GetButtonDown("Y_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
        {
            typeOfUnit = 3;
        }

        if (Input.GetButtonDown("Fire " + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing && setNb == 1)
        {
            typeOfUnit = 4;
        }

        if (Input.GetButtonDown("B_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing && setNb == 1)
        {
            typeOfUnit = 5;
        }

        if (Input.GetButtonDown("X_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing && setNb == 1)
        {
            typeOfUnit = 6;
        }

        if (Input.GetButtonDown("Y_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing && setNb == 1)
        {
            typeOfUnit = 7;
        }

        // DEBUG
        if (Input.GetKeyDown(KeyCode.S))
        {
            corSpawnUnits(0);
        }

    }

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
        GameObject prefabOfUnit = Instantiate(units[typeOfUnit], transform.position, transform.rotation) as GameObject;
        Unit unit = prefabOfUnit.GetComponent<Unit>();
        NavMeshAgent nav = prefabOfUnit.GetComponent<NavMeshAgent>();
        unit._playerId = _playerId;
        nav.SetDestination(waypoint.pos);
        unit._enemyMotherBase = targetBase;
        unit.waypointDest = waypoint;
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
