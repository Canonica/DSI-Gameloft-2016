﻿using UnityEngine;
using System.Collections;

public class Motherbase : Entity
{

    float cursorSensibility = 1;
    public Vector3 directionAttack;
    public CursorMovement cursor;

    [Header("Spawner Option")]
    public GameObject[] units;
    public float delay;
    public int unitSpawn;
    public GameObject obj;
    public GameObject[] waypoints = { null, null, null };
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
        waypoints[0] = GameObject.Find("wpTop");
        waypoints[1] = GameObject.Find("wpMid");
        waypoints[2] = GameObject.Find("wpBot");
        typeOfUnit = 0;
    }

    public override void Update()
    {
        base.Update();
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

        Debug.Log(setNb);

        if (GameManager.instance.currentGamestate == GameManager.gameState.Playing && !spawning)
        {
            StartCoroutine(Spawner());
            spawning = true;
        }

        if (Input.GetButtonDown("Fire " + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing && setNb ==0)
        {
            typeOfUnit = 0;
        }

        if (Input.GetButtonDown("B_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing && setNb == 0)
        {
            typeOfUnit = 1;
        }

        if (Input.GetButtonDown("X_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing && setNb == 0)
        {
            typeOfUnit = 2;
        }

        if (Input.GetButtonDown("Y_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing && setNb == 0)
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

    IEnumerator Spawner()
    {
        while (_life > 0)
        {
            corSpawnUnits(typeOfUnit);
            yield return new WaitForSeconds(units[typeOfUnit].GetComponent<Unit>()._hatchTime);
        }
    }

    //void spawnUnits(int index)
    //{
    //    GameObject obj = Instantiate(units[index], transform.position, transform.rotation) as GameObject;
    //    obj.GetComponent<Unit>()._playerId = idPlayer;
    //    obj.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].transform.position);
    //    obj.GetComponent<Unit>()._enemyMotherBase = targetBase;
    //    obj.GetComponent<Unit>()._Lane = waypoints[2].transform.position;
    //    obj.transform.parent = transform;
    //}

    public void getDamage(int dmg)
    {
        if (dmg > _life)
        {
            _life = 0;
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
        nav.SetDestination(waypoints[1].transform.position);
        unit._enemyMotherBase = targetBase;
        unit._Lane = waypoints[1].transform.position;
        prefabOfUnit.transform.parent = transform;
    }
}
