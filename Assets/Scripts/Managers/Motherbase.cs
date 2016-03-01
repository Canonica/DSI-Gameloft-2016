using UnityEngine;
using System.Collections;

public class Motherbase : MonoBehaviour {
    public int idPlayer;
    int life;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void getDamage(int dmg)
    {
        if (dmg > life)
        {
            life = 0;
            // call lost
        }
        else
        {
            if(dmg > 0)
            life -= dmg;
        }
    }
}
