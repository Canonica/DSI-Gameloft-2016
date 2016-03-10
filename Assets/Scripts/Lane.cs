using UnityEngine;
using System.Collections;

public class Lane : MonoBehaviour {
    public Waypoint firstP1Waypoint;
    public Waypoint firstP2Waypoint;
    public int num;

    public Waypoint getFirst(int id)
    {
        if (id == 1)
        {
            return firstP1Waypoint;
        }
        else
        {
            return firstP2Waypoint;
        }
    }
    
}
