using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BumpJumper : MonoBehaviour {

    public List<GameObject> bumpList;
    public void OnTriggerEnter(Collider parOther)
    {
        if (parOther.CompareTag("Unit"))
        {
            if (bumpList.IndexOf(parOther.gameObject) < 0)
                bumpList.Add(parOther.gameObject);
        }
    }

    public void OnTriggerExit(Collider parOther)
    {
        bumpList.Remove(parOther.gameObject);
    }
}
