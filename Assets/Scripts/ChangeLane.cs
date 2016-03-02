using UnityEngine;
using System.Collections;

public class ChangeLane : MonoBehaviour {
    int id=0;
    public float delayMove;
    public bool canMove;
    public int currentWP=1;
    public GameObject[] waypoints;
    public GameObject cursor;
    Motherbase mBase;
    void Awake()
    {
        mBase = GetComponent<Motherbase>();
        waypoints = new GameObject[3];
        waypoints[2] = GameObject.Find("wpTop");
        waypoints[1] = GameObject.Find("wpMid");
        waypoints[0] = GameObject.Find("wpBot");
        cursor.transform.position = waypoints[currentWP].transform.position;
        applyChange();
    }

	// Use this for initialization
	void Start () {
        canMove = true;
        id = GetComponent<Motherbase>()._playerId;
	}
	
	// Update is called once per frame
	void Update () {
        if (id != 0)
        {
            
            float h = Input.GetAxisRaw("L_YAxis_" + id);
            //float v = Input.GetAxisRaw("L_XAxis_" + id);
            if (h <= -0.9f)
            {
                if (canMove)
                    StartCoroutine(moveTop());
            }
            if (h >= 0.9f)
            {
                if (canMove)
                    StartCoroutine(moveBot());
            }
            if (h < 0.1f && h > -0.1f)
            {
                canMove = true;
                StopAllCoroutines();
            }
        }
    }

    IEnumerator moveTop()
    {
        canMove = false;
        if (currentWP + 1 < waypoints.Length)
        {
            currentWP++;
            
            Debug.Log(currentWP);
        }
        else
        {
            currentWP = 0;
        }
        cursor.transform.position = waypoints[currentWP].transform.position;
        applyChange();
        yield return new WaitForSeconds(delayMove);
        canMove = true;
    }

    IEnumerator moveBot()
    {
        canMove = false;
        if (currentWP - 1 >= 0)
        {
            currentWP--;
            
            Debug.Log(currentWP);
        }
        else
        {
            currentWP = waypoints.Length-1;
        }
        cursor.transform.position = waypoints[currentWP].transform.position;
        applyChange();
        yield return new WaitForSeconds(delayMove);
        canMove = true;
        
    }

    void applyChange()
    {
        mBase.waypoint = waypoints[currentWP].transform.position;
    }
}
