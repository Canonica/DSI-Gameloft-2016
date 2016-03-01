using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    Motherbase mBase;

	// Use this for initialization
	void Start () {
        //CreateBase();

    }

    void CreateBase()
    {
        GameObject objbase = Resources.Load("BaseBuilding") as GameObject;
        objbase.transform.position = transform.position;
        objbase.transform.parent = transform;
        objbase.AddComponent<Motherbase>();
        mBase = objbase.GetComponent<Motherbase>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
