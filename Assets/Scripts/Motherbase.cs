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
<<<<<<< HEAD
    public GameObject[] waypoints= {null,null,null };
    public GameObject targetBase;
    void Start()
    {
        waypoints[0] = GameObject.Find("wpTop");
        waypoints[1] = GameObject.Find("wpMid");
        waypoints[2] = GameObject.Find("wpBot");
    }
	
=======

    public GameObject obj;

    bool spawning;

	// Use this for initialization
	void Awake () {
        cursor.id = idPlayer;
        life = 10;
	}
	

	// Update is called once per frame
>>>>>>> refs/remotes/origin/Rodrigue
	void Update () {
        if (Input.GetButtonDown("Fire "+idPlayer) && GameManager.instance.currentGamestate == GameManager.gameState.Playing )
        {
            StartCoroutine(Spawner(units[0]));     
        }
<<<<<<< HEAD

        // DEBUG
        if (Input.GetKeyDown(KeyCode.S))
        {
            spawnUnits(0);
            Debug.Log("unit");
=======
        transform.position = new Vector3(cursor.transform.position.x, transform.position.y, transform.transform.position.z);

    }

    IEnumerator Spawner(GameObject typeOfUnit)
    {

        while (life > 0)
        {
            StartCoroutine(corSpawnUnits(typeOfUnit, 0));
            yield return new WaitForSeconds(typeOfUnit.GetComponent<Unit>()._hatchTime);
>>>>>>> refs/remotes/origin/Rodrigue
        }
    }
    

<<<<<<< HEAD
    void spawnUnits(int index)
    {
        GameObject obj = Instantiate(units[index], transform.position, transform.rotation) as GameObject;
        obj.GetComponent<Unit>()._playerId = idPlayer;
        obj.GetComponent<NavMeshAgent>().SetDestination(waypoints[1].transform.position);
        obj.GetComponent<Unit>()._enemyMotherBase = targetBase;
        obj.GetComponent<Unit>()._Lane = waypoints[1].transform.position;
        obj.transform.parent = transform;
    }
=======
>>>>>>> refs/remotes/origin/Rodrigue

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

    IEnumerator corSpawnUnits(GameObject typeOfUnit, int nbOfUnits)
    {
        typeOfUnit = Instantiate(units[nbOfUnits], transform.position, transform.rotation) as GameObject;
        typeOfUnit.GetComponent<Unit>()._playerId = idPlayer;
        typeOfUnit.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        typeOfUnit.GetComponent<Unit>()._enemyMotherBase = target;
        yield return new WaitForSeconds(typeOfUnit.GetComponent<Unit>()._hatchTime);
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
