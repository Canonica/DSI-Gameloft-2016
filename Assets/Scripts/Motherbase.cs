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
    public GameObject[] waypoints= {null,null,null };
    public GameObject targetBase;
    void Start()
    {
        waypoints[0] = GameObject.Find("wpTop");
        waypoints[1] = GameObject.Find("wpMid");
        waypoints[2] = GameObject.Find("wpBot");
    }
	
	void Update () {
        if (Input.GetButtonDown("Fire "+idPlayer))
        {
            spawnUnits(0);
        }

        // DEBUG
        if (Input.GetKeyDown(KeyCode.S))
        {
            spawnUnits(0);
            Debug.Log("unit");
        }
    }
    

    void spawnUnits(int index)
    {
        GameObject obj = Instantiate(units[index], transform.position, transform.rotation) as GameObject;
        obj.GetComponent<Unit>()._playerId = idPlayer;
        obj.GetComponent<NavMeshAgent>().SetDestination(waypoints[1].transform.position);
        obj.GetComponent<Unit>()._enemyMotherBase = targetBase;
        obj.GetComponent<Unit>()._Lane = waypoints[1].transform.position;
        obj.transform.parent = transform;
    }

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

    /*void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "unit")
        {
            getDamage(1);

            //get damage from unit
        }
    }*/


}
