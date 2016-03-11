using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PauseCanvas : MonoBehaviour
{
	public Text playerText;
	public Button resume;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void SetPlayerText (int i)
	{
		playerText.text = "PLAYER " + i;
	}

	public void Focus ()
	{
		resume.Select ();
	}

	public void MyDebug ()
	{
		Debug.Log ("lol");
	}

	public void HowToPlay ()
	{
		if (GameManager.instance.currentGamestate == GameManager.gameState.Pause) {
            GameObject.Find("HelpBox").GetComponent<ModularNavigation>().Inputable = true;
            GetComponent<Animator> ().SetBool ("HowToPlay", true);
            

            GameManager.instance.NoInput ();
		}
	}

	public void UnHowToPlay ()
	{
		GetComponent<Animator> ().SetBool ("HowToPlay", false);
        GetComponentInChildren<ModularNavigation>().right = 0;

        GetComponentInChildren<ModularNavigation>().StopAllCoroutines();
        GetComponentInChildren<ModularNavigation>().Inputable = false;
        GameManager.instance.SetInput ();
	}

    public void Unpause()
    {
        GameManager.instance.Unpause();
    }
    public void Quit()
    {
        GameManager.instance.Quit();
    }
    public void Restart()
    {
        GameManager.instance.Restart();
    }
}
