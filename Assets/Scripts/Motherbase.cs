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

	// Use this for initialization
	void Awake () {
        cursor.id = idPlayer;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire "+idPlayer))
        {
            Debug.Log(idPlayer +" "+transform.position);
            spawnUnits(0);
        }
        transform.position = new Vector3(cursor.transform.position.x, transform.position.y, transform.transform.position.z);
    }

    IEnumerator Spawner()
    {
        while (life > 0)
        {
            yield return new WaitForSeconds(delay);
            spawnUnits(unitSpawn);
        }
    }

    void spawnUnits(int index)
    {
        GameObject obj = Instantiate(units[index], transform.position, transform.rotation) as GameObject;
        obj.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
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

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "unit")
        {
            getDamage(1);

            //get damage from unit
        }
    }


}
