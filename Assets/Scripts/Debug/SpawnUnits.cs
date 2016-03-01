using UnityEngine;

public class SpawnUnits : MonoBehaviour {

    GameObject _unitPrefab;
    
    void Start()
    {
        _unitPrefab = Resources.Load("Entities/Unit") as GameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Spawn();
        }
    }
    
    public void Spawn()
    {
        GameObject unit = Instantiate(_unitPrefab, transform.position + Vector3.up * 2.0f, Quaternion.identity) as GameObject;
    }
}
