using UnityEngine;
using System.Collections;
using DG.Tweening;

public class doshaketeo : MonoBehaviour {
    float shake = 0.1f;
    public GameObject queen;
	// Use this for initialization
	void Start () {
        queen.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void shakeCamera()
    {
        Camera.main.DOKill(true);
        Camera.main.DOShakePosition(0.3f,shake);
        shake += 0.05f;
        
    }

    public void shakeCamForce(float force)
    {
        Camera.main.DOKill(true);
        Camera.main.DOShakePosition(force);

    }

    public void endAnimStart()
    {
        
        this.gameObject.SetActive(false);
        queen.SetActive(true);
    }

}
