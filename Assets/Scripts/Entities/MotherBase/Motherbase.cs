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

    public int[] maxNbOfUnits;
    public int[] currentNbOfUnits;


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
    }

    public override void FixedUpdate()
    {

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
        // DEBUG
        if (Input.GetKey(KeyCode.S) && _playerId == 2)
        {
            currentNbOfUnits[0] = 50;
            corSpawnUnits(0);
        }
        if (Input.GetKey(KeyCode.D)&& _playerId ==1)
        {
            currentNbOfUnits[1] = 50;
            corSpawnUnits(1);
        }

        if (GameManager.instance.currentGamestate == GameManager.gameState.Playing)
        {
            if (!spawning)
            {
                StartCoroutine(loadUnit(0));
                StartCoroutine(loadUnit(1));
                //StartCoroutine(loadUnit(2));
                //StartCoroutine(loadUnit(3));
                spawning = true;
            }
            
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

            if (Input.GetButtonDown("Fire " + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
            {
                Debug.Log("Test");
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

            
        }
        base.FixedUpdate();
    }
    //IEnumerator Spawner()
    //{
    //    while (_life > 0)
    //    {
    //        corSpawnUnits(typeOfUnit);
    //        yield return new WaitForSeconds(units[typeOfUnit].GetComponent<Unit>()._hatchTime);
    //    }
    //}


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
        if (currentNbOfUnits[typeOfUnit] > 0)
        {
            for (int i = 0; i < units[typeOfUnit].GetComponent<Unit>().groupSpawn; i++)
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
            currentNbOfUnits[typeOfUnit]--;
        }
        
    }

    IEnumerator loadUnit(int nbOfUnits)
    {
        while(_life > 0)
        {
            if (currentNbOfUnits[nbOfUnits] < maxNbOfUnits[nbOfUnits])
            {
                currentNbOfUnits[nbOfUnits]++;
            }
            yield return new WaitForSeconds(units[nbOfUnits].GetComponent<Unit>()._hatchTime);
        }
        
        
    }

}
