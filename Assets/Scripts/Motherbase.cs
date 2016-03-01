using UnityEngine;
using System.Collections;

public class Motherbase : MonoBehaviour {
    public int idPlayer;
    int life;

    public GameObject[] units;
    public Vector3 directionAttack;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            spawnUnits(0);
        }
	}

    void spawnUnits(int index)
    {
        GameObject obj = Instantiate(units[index], transform.position, transform.rotation) as GameObject;
        obj.GetComponent<UnitMovement>().direction = directionAttack;
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
            getDamage(0);

            //get damage from unit
        }
    }


}
