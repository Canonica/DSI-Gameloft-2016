using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public gameState currentGamestate;

    public enum gameState
    {
        Menu,
        Pause,
        GameOver,
        Playing
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

    public void EndGame()
    {
        
    }
}
