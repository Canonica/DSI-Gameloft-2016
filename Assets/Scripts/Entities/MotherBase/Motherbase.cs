using UnityEngine;
using System.Collections;

public class Motherbase : Entity {

    float cursorSensibility = 1;
    public GameObject[] units;
    public Vector3 directionAttack;
    public CursorMovement cursor;

    [Header("Spawner Option")]
    public float delay;
    public int unitSpawn;

	// Use this for initialization
	void Awake () {
        cursor.id = _playerId;
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        if (Input.GetButtonDown("Fire "+ _playerId))
        {
            spawnUnits(0);
        }

        transform.position = new Vector3(cursor.transform.position.x, transform.position.y, transform.transform.position.z);
    }

    IEnumerator Spawner()
    {
        while (_life > 0)
        {
            yield return new WaitForSeconds(delay);
            spawnUnits(unitSpawn);
        }
    }

    void spawnUnits(int index)
    {
        GameObject obj = Instantiate(units[index], transform.position, transform.rotation) as GameObject;
        obj.GetComponent<Unit>()._playerId = _playerId;
        obj.GetComponent<NavMeshAgent>().SetDestination(_enemyMotherBase.transform.position);
        obj.GetComponent<Unit>()._enemyMotherBase = _enemyMotherBase;
    }

    public void getDamage(int dmg)
    {
        if (dmg > _life)
        {
            _life = 0;
            // send defeat
        }
        else
        {
            if(dmg > 0)
                _life -= dmg;
        }
    }
}
