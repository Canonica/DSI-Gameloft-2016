﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{

    public static MenuManager instance = null;

    public GameObject MainMenuCanvas;
    public GameObject CreditsCanvas;
    public GameObject OptionsCanvas;
    public GameObject ConnectCanvas;

    public GameObject PauseCanvas;

    public CanvasGroup MainMenuCanvasGroup;
    public CanvasGroup CreditsCanvasGroup;
    public CanvasGroup OptionsCanvasGroup;
    public float fadeInSpeed;
    public float fadeOutSpeed;

    float delay = 0.5f;


    void Awake()
    {
        if (instance == null)
            instance = this;
        
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            MainMenuCanvas = GameObject.Find("MainMenuCanvas");
            CreditsCanvas = GameObject.Find("CreditsCanvas");
            OptionsCanvas = GameObject.Find("OptionsCanvas");

            MainMenuCanvasGroup = MainMenuCanvas.GetComponent<CanvasGroup>();
            CreditsCanvasGroup = CreditsCanvas.GetComponent<CanvasGroup>();
            OptionsCanvasGroup = OptionsCanvas.GetComponent<CanvasGroup>();

            foreach (Transform child in MainMenuCanvas.transform)
            {
                switch (child.name)
                {
                    case "PlayButton":
                        child.GetComponent<Button>().onClick.AddListener(() => { Play(); });
                        break;
                    case "OptionsButton":
                        child.GetComponent<Button>().onClick.AddListener(() => { ShowOptions(); });
                        break;
                    case "CreditsButton":
                        child.GetComponent<Button>().onClick.AddListener(() => { ShowCredits(); });
                        break;
                    case "QuitButton":
                        child.GetComponent<Button>().onClick.AddListener(() => { Quit(); });
                        break;
                    default :
                        break;
                }
            }
            OptionsCanvas.transform.FindChild("MainMenuButton").GetComponent<Button>().onClick.AddListener(() => { Main_Menu_From_Options(); });
            CreditsCanvas.transform.FindChild("MainMenuButton").GetComponent<Button>().onClick.AddListener(() => { Main_Menu_From_Credit(); });
           

            MainMenuCanvas.SetActive(false);
            CreditsCanvas.SetActive(false);
            OptionsCanvas.SetActive(false);
            
            StartCoroutine(fadeIn(MainMenuCanvasGroup, fadeInSpeed));
        }
        if (level == 2)
        {
            PauseCanvas = GameObject.Find("PauseCanvas");
        }
    }
    

    void Update()
    {

        /*if (delay != 0)
            delay = Mathf.Max(delay - Time.deltaTime, 0f);
        if (delay == 0)
        {

        }*/

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Bouton back");
            Application.Quit();
        }
    }

    public void Main_Menu()
    {

        Time.timeScale = 1;
        GameManager.GetInstance().currentGamestate = GameManager.gameState.Menu;
        ChangeCanvas(MainMenuCanvasGroup, MainMenuCanvasGroup);
        SceneManager.LoadScene(1);
    }

    public void Play()
    {
        Time.timeScale = 1;
        

        GameManager.GetInstance().currentGamestate = GameManager.gameState.Waiting;
        SceneManager.LoadScene(2);
    }
    

    public void Quit()
    {
        Application.Quit();
    }

    public void Main_Menu_From_Credit()
    {
        HideAll();
        MainMenuCanvas.SetActive(true);
        StartCoroutine(fadeIn(MainMenuCanvasGroup, fadeInSpeed));
        StartCoroutine(fadeOut(CreditsCanvasGroup));

    }

    

    public void Main_Menu_From_Options()
    {
        HideAll();
        MainMenuCanvas.SetActive(true);
        StartCoroutine(fadeIn(MainMenuCanvasGroup, fadeInSpeed));
        //StartCoroutine(fadeOut(OptionsCanvasGroup));

    }

    public void ShowOptions()
    {
        HideAll();
        OptionsCanvas.SetActive(true);
        StartCoroutine(fadeIn(OptionsCanvasGroup, fadeInSpeed));
        StartCoroutine(fadeOut(MainMenuCanvasGroup));
    }
    

    public void ShowCredits()
    {
        HideAll();
        CreditsCanvas.SetActive(true);
        StartCoroutine(fadeIn(CreditsCanvasGroup, fadeInSpeed));
        StartCoroutine(fadeOut(MainMenuCanvasGroup));
    }

    public void Restart()
    {
        GameManager.GetInstance().currentGamestate = GameManager.gameState.Playing;
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void Resume()
    {
        PauseCanvas.SetActive(false);
        GameManager.GetInstance().currentGamestate = GameManager.gameState.Playing;
        Time.timeScale = 1;
    }

    public void HideAll()
    {
        MainMenuCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
    }

    IEnumerator fadeIn(CanvasGroup currentCanva, float fadeInSpeed)
    {
        //yield return new WaitForSeconds(1f);
        while (currentCanva.alpha < 1.0f)
        {
            currentCanva.gameObject.SetActive(true);
            currentCanva.alpha += fadeInSpeed;
            yield return new WaitForSeconds(0.01f);
        }


    }
    IEnumerator fadeOut(CanvasGroup currentCanva)
    {
        while (currentCanva.alpha > 0.0f)
        {
            currentCanva.alpha -= 0.05f;
            currentCanva.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.01f);
        }
        currentCanva.gameObject.SetActive(false);

    }

    private void ChangeCanvas(CanvasGroup canvasOut, CanvasGroup canvasIn)
    {
        StartCoroutine(fadeOut(canvasOut));
        StartCoroutine(fadeIn(canvasIn, fadeInSpeed));
    }

    private void PutPause()
    {
        Time.timeScale = 0;
        GameManager.GetInstance().currentGamestate = GameManager.gameState.Pause;
    }
}