using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public gameState currentGamestate;

    GameObject _panelWaiting;
    CanvasGroup _panelWaitingGroup;

    GameObject _panelBeforeFight;
    CanvasGroup _panelBeforeFightGroup;

    private bool isPlaying = false;

    public enum gameState
    {
        Menu,
        Pause,
        GameOver,
        Playing,
        Waiting
    };
    public static GameManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        Debug.Log(currentGamestate);
        if(currentGamestate == gameState.Waiting)
        {
            Time.timeScale = 0;
        }
        else if(currentGamestate == gameState.Playing && !isPlaying)
        {
            LaunchGame();
            isPlaying = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            currentGamestate = gameState.Playing;
        }

    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 3)
        {
            _panelWaiting = GameObject.Find("PanelWaiting");
            _panelWaitingGroup = _panelWaiting.GetComponent<CanvasGroup>();


            _panelBeforeFight = GameObject.Find("PanelBeforeFight");
            _panelBeforeFightGroup = _panelBeforeFight.GetComponent<CanvasGroup>();
            _panelBeforeFight.SetActive(false);
        }
    }

    public void GameOver(int parPlayerWin)
    {
        Debug.Log("The Player" + parPlayerWin + "wins the game");
    }

    void LaunchGame()
    {
        StartCoroutine(fadeOut(_panelWaitingGroup));
        _panelBeforeFight.SetActive(true);
        _panelWaiting.SetActive(false);
        _panelBeforeFight.GetComponentInChildren<BeforeFight>().StartCD();
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
}
