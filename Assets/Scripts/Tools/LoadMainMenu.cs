using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("MainMenu", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
