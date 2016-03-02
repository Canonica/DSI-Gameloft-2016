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

	// Use this for initialization
	void Awake () {
        life = 10;
	}
	

	// Update is called once per frame

    void Start()
    {
        waypoints[0] = GameObject.Find("wpTop");
        waypoints[1] = GameObject.Find("wpMid");
        waypoints[2] = GameObject.Find("wpBot");
    }
	

	void Update () {
        if (Input.GetButtonDown("Fire "+idPlayer) && GameManager.instance.currentGamestate == GameManager.gameState.Playing )
        {
            StartCoroutine(Spawner(units[0]));     
        }

        // DEBUG
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Spawner(units[0]));
        }
    }

    IEnumerator Spawner(GameObject typeOfUnit)
    {

        while (life > 0)
        {
            StartCoroutine(corSpawnUnits(typeOfUnit, 0));
            yield return new WaitForSeconds(typeOfUnit.GetComponent<Unit>()._hatchTime);
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

    IEnumerator corSpawnUnits(GameObject prefabOfUnit, int typeOfUnit)
    {
        prefabOfUnit = Instantiate(units[typeOfUnit], transform.position, transform.rotation) as GameObject;
        prefabOfUnit.GetComponent<Unit>()._playerId = idPlayer;
        prefabOfUnit.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].transform.position);
        prefabOfUnit.GetComponent<Unit>()._enemyMotherBase = targetBase;
        prefabOfUnit.GetComponent<Unit>()._Lane = waypoints[2].transform.position;
        prefabOfUnit.transform.parent = transform;
        yield return new WaitForSeconds(prefabOfUnit.GetComponent<Unit>()._hatchTime);
    }

    /*void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "unit")
        {
            getDamage(1);

            //get damage from unit
        }
    }*/


}
