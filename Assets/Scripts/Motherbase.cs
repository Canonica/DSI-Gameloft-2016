using UnityEngine;
using System.Collections;

public class Motherbase : MonoBehaviour {
    public int idPlayer;
    int life;
    float cursorSensibility = 1;
    public GameObject[] units;
    public Vector3 directionAttack;
    public CursorMovement cursor;
	// Use this for initialization
	void Awake () {
        cursor.id = idPlayer;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            spawnUnits(0);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, cursor.transform.position.z);
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
            getDamage(1);

            //get damage from unit
        }
    }


}
