using UnityEngine;
using System.Collections;

public class OutOfMap : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        Destroy(col.collider.gameObject);
    }
}
