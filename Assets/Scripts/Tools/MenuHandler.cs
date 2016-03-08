using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
	[HideInInspector]
	public int indice;
	bool dirUp, dirDown, dirLeft, dirRight;
	bool stillUp, stillDown, stillLeft, stillRight;

	public List<Button> buttonArray = new List<Button> ();


	//public AudioClip audioclipMenuMove;
	//private GameObject speakerMenuMove;
	// Use this for initialization
	void Start ()
	{
		indice = 0;
	}

	void Update ()
	{
		if (GameManager.instance.currentGamestate == GameManager.gameState.Menu) {
			if (Input.GetAxis ("L_YAxis_0") > 0 || Input.GetKeyDown ("down")) {
				//vers le bas
				if (dirDown == false) {
					dirDown = true;
					dirUp = false;

					StopAllCoroutines ();
					StartCoroutine (Wait1sec ());



				}
			} else if (Input.GetAxis ("L_YAxis_0") < 0 || Input.GetKeyDown ("up")) {
				//vers le haut
				if (dirUp == false) {
					dirUp = true;
					dirDown = false;
					StopAllCoroutines ();
					StartCoroutine (Wait1sec ());

				}
			} else {
				dirUp = false;
				dirDown = false;

				StopAllCoroutines ();
			}

			Debug.Log (buttonArray [indice]);
			buttonArray [indice].Select ();
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
		ChangeIndice ();
        
		yield return null;
		/*while (stillUp == dirUp && stillDown == dirDown && stillLeft == dirLeft && stillRight == dirRight ) {
			//move again
			
			ChangeIndice();
            //SoundManagerEvent.emit(SoundManagerType.MENUMOVE);
		}*/
        
	}

	void ChangeIndice ()
	{
		//speakerMenuMove = SoundManager.Instance.playSound(audioclipMenuMove, 100);
		//speakerMenuMove.GetComponent<AudioSource>().loop = false;
		if (indice == 0) {
           
			if (stillDown == true) {
				indice++;
			}
			
		} else if (indice != 0) {

			if (stillUp == true) {
				indice--;
			}
			if (stillDown == true && indice < buttonArray.Count - 1) {
				indice++;
			}

		}
	}

}
