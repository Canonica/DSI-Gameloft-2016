using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//using UnityEngine.SceneManagement;

public class LobbyInput : MonoBehaviour
{
	[HideInInspector]
	public int indice;
	bool dirUp, dirDown, dirLeft, dirRight;
	bool stillUp, stillDown, stillLeft, stillRight;


	float Readiness_1 = 0;
	float Readiness_2 = 0;

	bool isReady_1 = false;
	bool isReady_2 = false;

	Image Img_Readiness_1;
	Image Img_Readiness_2;
	GameObject HowToPlay;


	int tick = 0;

	//public AudioClip audioclipMenuMove;
	//private GameObject speakerMenuMove;
	// Use this for initialization
	void Start ()
	{
		indice = 0;

		Img_Readiness_1 = GameObject.Find ("Readiness1").GetComponent<Image> ();
		Img_Readiness_2 = GameObject.Find ("Readiness2").GetComponent<Image> ();
		HowToPlay = GameObject.Find ("HowToPlay");
//		Img_Readiness_1 = GameObject.Find ("Readines1");
	}

	void Update ()
	{
		tick++;
		if (GameManager.instance.currentGamestate == GameManager.gameState.Menu && tick > 30) {
			
			if (Input.GetAxis ("A_button_1") > 0) {
				Readiness_1 += Time.deltaTime;
				if (Readiness_1 / 0.5f <= 1) {
					Img_Readiness_1.fillAmount = Readiness_1 / 0.5f;
				} else if (!isReady_1) {
					isReady_1 = true;
					CheckReadiness ();
				}

			} else if (!isReady_1) {
				Readiness_1 = 0;
				Img_Readiness_1.fillAmount = 0;
			}

			if (Input.GetAxis ("A_button_2") > 0 || Input.GetKey (KeyCode.T)) {
				Readiness_2 += Time.deltaTime;
				if (Readiness_2 / 0.5f <= 1) {
					Img_Readiness_2.fillAmount = Readiness_2 / 0.5f;
				} else if (!isReady_2) {
					isReady_2 = true;
					CheckReadiness ();
				}

			} else if (!isReady_2) {
				Readiness_2 = 0;
				Img_Readiness_2.fillAmount = 0;
			}
		}
            
            

		/*if (Input.GetButtonDown ("A_button_0") || Input.GetKeyDown("return")) {
			
			switch (indice){
			case 0:
                    //buttonArray[0].onClick.Invoke();

                    //SceneManager.LoadScene("Intro_perso");
                    //Application.LoadLevel("Intro_perso");

                    //SoundManagerEvent.emit(SoundManagerType.MENUSELECTION);
                    break;
			case 1:
                //SoundManagerEvent.emit(SoundManagerType.MUTE);
				break;
			case 2:
                    
                    //Application.LoadLevel("Menu_Credits");
                    //SoundManagerEvent.emit(SoundManagerType.MENUSELECTION);
                    break;
			case 3:
                    //SoundManagerEvent.emit(SoundManagerType.MENUSELECTION);
				Application.Quit();
				break;

//			default:
//				break;
			}
			
		}
        if (Input.GetMouseButtonDown(0))
        {

        }*/

	}


	IEnumerator Wait1sec ()
	{
		
		stillUp = dirUp;
		stillDown = dirDown;
        
		yield return null;
		/*while (stillUp == dirUp && stillDown == dirDown && stillLeft == dirLeft && stillRight == dirRight ) {
			//move again
			
			ChangeIndice();
            //SoundManagerEvent.emit(SoundManagerType.MENUMOVE);
		}*/
        
	}


	void CheckReadiness ()
	{
		if (isReady_1 && isReady_2) {
			HowToPlay.GetComponent<Animator> ().SetTrigger ("In");

			Invoke ("Launch", 5f);
		}
	}

	void Launch ()
	{
		Time.timeScale = 1;


		GameManager.GetInstance ().currentGamestate = GameManager.gameState.Waiting;
		SceneManager.LoadScene (2);
	}

}
