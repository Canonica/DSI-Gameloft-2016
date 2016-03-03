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

    public Motherbase player1;
    public Motherbase player2;
    

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

    void OnLevelWasLoaded(int level)
    {
        if(level == 2)
        {
            player1 = GameObject.Find("Player 1").GetComponent<Motherbase>();
            player2 = GameObject.Find("Player 2").GetComponent<Motherbase>();
        }
    }

    public void GameOver(int parPlayerWin)
    {
        Debug.Log("The Player" + parPlayerWin + "wins the game");
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
