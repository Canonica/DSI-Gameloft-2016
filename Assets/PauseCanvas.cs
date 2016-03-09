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
		if (GameManager.instance.currentGamestate == GameManager.gameState.Pause)
			GetComponent<Animator> ().SetBool ("HowToPlay", true);	
	}

	public void UnHowToPlay ()
	{
		GetComponent<Animator> ().SetBool ("HowToPlay", false);	
	}
}
