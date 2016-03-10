using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {
    public Vector3 direction;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction.normalized*Time.deltaTime;
	}
}
