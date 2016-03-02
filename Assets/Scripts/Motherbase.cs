using UnityEngine;
using System.Collections;

public class Motherbase : MonoBehaviour {
    public int idPlayer;
    int life;
    float cursorSensibility = 1;
    public GameObject[] units;
    public Vector3 directionAttack;
    public CursorMovement cursor;
    public GameObject target;

    [Header("Spawner Option")]
    public float delay;
    public int unitSpawn;

    public GameObject obj;

    bool spawning;

	// Use this for initialization
	void Awake () {
        cursor.id = idPlayer;
        life = 10;
	}
	

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire "+idPlayer) && GameManager.instance.currentGamestate == GameManager.gameState.Playing )
        {
            StartCoroutine(Spawner(units[0]));     
        }
        transform.position = new Vector3(cursor.transform.position.x, transform.position.y, transform.transform.position.z);

    }

    IEnumerator Spawner(GameObject typeOfUnit)
    {

        while (life > 0)
        {
            StartCoroutine(corSpawnUnits(typeOfUnit, 0));
            yield return new WaitForSeconds(typeOfUnit.GetComponent<Unit>()._hatchTime);
        }
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
