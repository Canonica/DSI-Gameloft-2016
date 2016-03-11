using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private int j;

	public static GameManager instance = null;
	public gameState currentGamestate;

	GameObject _panelWaiting;
	CanvasGroup _panelWaitingGroup;

	GameObject _panelBeforeFight;
	CanvasGroup _panelBeforeFightGroup;

	public Motherbase player1;
	public Motherbase player2;

	private bool Pausable = true;
	private UnityStandardAssets.ImageEffects.ColorCorrectionCurves CameraEffect;
	private GameObject PauseCanvas;
	private int pauseButton = -1;
	private int pauser = -1;


	public GameObject ESystem;

    public GameObject pauseCanvas;

	public enum gameState
	{
		Menu,
		Pause,
		GameOver,
		Playing,
		PauseOut,
		Waiting}

	;


	IEnumerator setPausable (float delay)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + delay) {
			yield return null;
		}
			
		Pausable = true;
	}

	//	IEnumerator waitForPress (int toCheck)
	//	{
	//		return new WaitForSeconds (0.5f);
	//		if (Input.GetAxisRaw ("Start_button_"+toCheck) > 0.9f) {
	//			pauseButton = toCheck1;
	//		}
	//	}
	//

	public static GameManager GetInstance ()
	{
		return instance;
	}

	void Awake ()
	{
		instance = this;

		//DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{
		
		
		ESystem = GameObject.Find ("EventSystem");
	}

    void OnLevelLoaded(int level)
    {
        if (level == 2)
        {
            CameraEffect = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionCurves>();
            ESystem = GameObject.Find("EventSystem");
            PauseCanvas = GameObject.Find("PauseCanvas");

        }
        
    }

	void Update ()
	{

       // CameraEffect = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionCurves>();
        if (CameraEffect == null && Application.loadedLevel == 2)
        {
            CameraEffect = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionCurves>();
        }

        if(ESystem == null && Application.loadedLevel == 2)
        {
            ESystem = GameObject.Find("EventSystem");
            PauseCanvas = GameObject.Find("PauseCanvas");
        }

            if (currentGamestate == gameState.Pause || currentGamestate == gameState.PauseOut) {
			Time.timeScale = (Time.timeScale * 29 + 0.01f) / 30;
			CameraEffect.saturation = (CameraEffect.saturation * 29 + 0.1f) / 30;
		} else if (currentGamestate == gameState.Playing) {
			Time.timeScale = (Time.timeScale * 29 + 1) / 30;
			CameraEffect.saturation = (CameraEffect.saturation * 29 + 1) / 30;
		}


		if (Input.GetAxisRaw ("Start_button_0") > 0.9f)
			pauseButton = 1;
		else if (Input.GetAxisRaw ("Start_button_1") > 0.9f)
			pauseButton = 2;
		else
			pauseButton = 0;

		if ((Input.GetKey (KeyCode.P) || pauseButton > 0) && Pausable) {
			if (currentGamestate == gameState.Pause && pauseButton == pauser) {
				Unpause ();
				Pausable = false;
				StartCoroutine (setPausable (0.5f));
			} else if (currentGamestate == gameState.Playing) {
				ESystem.GetComponent<StandaloneInputModule> ().verticalAxis = "L_YAxis_inv_" + (pauseButton - 1);
				ESystem.GetComponent<StandaloneInputModule> ().submitButton = "Submit_" + (pauseButton - 1);
				ESystem.GetComponent<StandaloneInputModule> ().cancelButton = "Cancel_" + (pauseButton - 1);
				currentGamestate = gameState.Pause;
				Pausable = false;
				PauseCanvas.GetComponent<Animator> ().SetBool ("isIn", true);
				PauseCanvas.GetComponent<PauseCanvas> ().Focus ();
				PauseCanvas.GetComponent<PauseCanvas> ().SetPlayerText (pauseButton);
				pauser = pauseButton;
				StartCoroutine (setPausable (0.5f));
			}
		} 
	}

	public void SetInput ()
	{
		ESystem.GetComponent<StandaloneInputModule> ().verticalAxis = "L_YAxis_inv_" + (pauser - 1);
		ESystem.GetComponent<StandaloneInputModule> ().submitButton = "Submit_" + (pauser - 1);
		ESystem.GetComponent<StandaloneInputModule> ().cancelButton = "Cancel_" + (pauser - 1);
	}

	public void NoInput ()
	{
		ESystem.GetComponent<StandaloneInputModule> ().verticalAxis = "Null";
		ESystem.GetComponent<StandaloneInputModule> ().submitButton = "Submit_" + (pauser - 1);
		ESystem.GetComponent<StandaloneInputModule> ().cancelButton = "Cancel_" + (pauser - 1);
	}

	void OnLevelWasLoaded (int level)
	{
		if (level == 2) {
			player1 = GameObject.Find ("Player 1").GetComponent<Motherbase> ();
			player2 = GameObject.Find ("Player 2").GetComponent<Motherbase> ();
		}
	}

	public void GameOver (int parPlayerWin)
	{
		Debug.Log ("The Player" + parPlayerWin + "wins the game");
	}


	IEnumerator fadeOut (CanvasGroup currentCanva)
	{
		while (currentCanva.alpha > 0.0f) {
			currentCanva.alpha -= 0.05f;
			currentCanva.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.01f);
		}
		currentCanva.gameObject.SetActive (false);
	}

	public void Unpause ()
	{
		ESystem.GetComponent<StandaloneInputModule> ().verticalAxis = "Vertical";
		ESystem.GetComponent<StandaloneInputModule> ().submitButton = "Submit";
		ESystem.GetComponent<StandaloneInputModule> ().cancelButton = "Cancel";
		StartCoroutine (GoPlay (0.2f));

        PauseCanvas.GetComponentInChildren<ModularNavigation>().right = 0;
        PauseCanvas.GetComponentInChildren<ModularNavigation>().StopAllCoroutines();
        PauseCanvas.GetComponentInChildren<ModularNavigation>().Inputable = false;

        PauseCanvas.GetComponent<Animator> ().SetBool ("isIn", false);
		PauseCanvas.GetComponent<PauseCanvas> ().SetPlayerText (pauseButton);
		PauseCanvas.GetComponent<Animator> ().SetBool ("HowToPlay", false);	

	}


	public void Restart ()
	{
		if (currentGamestate == gameState.Pause) {
            currentGamestate = gameState.Playing;
            SceneManager.LoadScene (2);
			
		}

	}

	public void Quit ()
	{
		if (currentGamestate == gameState.Pause) {
			SceneManager.LoadScene (1);
			currentGamestate = gameState.Menu;
		}
	}

	IEnumerator GoPlay (float delay)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + delay) {
			yield return null;
		}

		currentGamestate = gameState.Playing; 
	}
}
