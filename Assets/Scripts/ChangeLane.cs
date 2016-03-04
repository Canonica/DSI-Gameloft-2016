using UnityEngine;
using System.Collections;

public class ChangeLane : MonoBehaviour {
    int id=0;
    public float delayMove;
    public bool canMove;
    public int currentWP=1;
    public GameObject[] Lane;
    public GameObject cursor;
    Motherbase mBase;
    void Awake()
    {
        mBase = GetComponent<Motherbase>();
        Lane = new GameObject[3];
        Lane[2] = GameObject.Find("LaneTop");
        Lane[1] = GameObject.Find("LaneMid");
        Lane[0] = GameObject.Find("LaneBot");
        
        
    }

	// Use this for initialization
	void Start () {
        canMove = true;
        id = GetComponent<Motherbase>()._playerId;
        cursor.transform.position = Lane[currentWP].GetComponent<Lane>().getFirst(id).pos;
        applyChange();
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
        if (currentWP + 1 < Lane.Length)
        {
            currentWP++;
        }
        else
        {
            currentWP = 0;
        }
        cursor.transform.position = Lane[currentWP].transform.position;
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
            currentWP = Lane.Length-1;
        }
        cursor.transform.position = Lane[currentWP].transform.position;
        applyChange();
        yield return new WaitForSeconds(delayMove);
        canMove = true;
        
    }

    void applyChange()
    {
        mBase.waypoint = Lane[currentWP].GetComponent<Lane>().getFirst(id);
    }
}
