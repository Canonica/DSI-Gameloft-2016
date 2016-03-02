using UnityEngine;
using System.Collections;

public class Motherbase : MonoBehaviour {
    [Header("Base Option")]
    public int idPlayer;
    public int life;
    
    [Header("Spawner Option")]
    public GameObject[] units;
    public float delay;
    public int unitSpawn;
    public GameObject obj;
    public GameObject[] waypoints = { null, null, null };
    public GameObject targetBase;
    bool spawning;

    int typeOfUnit;

    // Use this for initialization
    void Awake () {
        life = 10;
        spawning = false;
	}
	

	// Update is called once per frame

    void Start()
    {
        waypoints[0] = GameObject.Find("wpTop");
        waypoints[1] = GameObject.Find("wpMid");
        waypoints[2] = GameObject.Find("wpBot");
    }
	

	void Update () {
        if (Input.GetButtonDown("Fire "+idPlayer) && GameManager.instance.currentGamestate == GameManager.gameState.Playing && !spawning)
        {
            StartCoroutine(Spawner());
            spawning = true;   
        }

        // DEBUG
        if (Input.GetKeyDown(KeyCode.S))
        {
            corSpawnUnits(0);
        }
    }

    IEnumerator Spawner()
    {

        while (life > 0)
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
        if (dmg > life)
        {
            life = 0;
            // send defeat
        }
        else
        {
            if(dmg > 0)
            life -= dmg;
        }
    }

    void corSpawnUnits(int typeOfUnit)
    {
        GameObject prefabOfUnit = Instantiate(units[typeOfUnit], transform.position, transform.rotation) as GameObject;
        Unit unit = prefabOfUnit.GetComponent<Unit>();
        NavMeshAgent nav = prefabOfUnit.GetComponent<NavMeshAgent>();
        unit._playerId = idPlayer;
        nav.SetDestination(waypoints[1].transform.position);
        unit._enemyMotherBase = targetBase;
        unit._Lane = waypoints[1].transform.position;
        prefabOfUnit.transform.parent = transform;
    }
}
