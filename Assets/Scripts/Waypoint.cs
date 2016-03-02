using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    
    public Vector3 pos;
    public Waypoint nextP1 = null;
    public Waypoint nextP2 = null;
    void Start()
    {
        pos = transform.position;
    }


    public Waypoint Next(int id)
    {
        if (id == 1)
        {
            return nextP1;
        }
        else
        {
            return nextP2;
        }
    }
}
