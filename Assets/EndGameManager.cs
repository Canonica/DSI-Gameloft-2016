using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour {
    public static EndGameManager instance = null;
    GameObject player1UI, player2UI;

    int[] damage;
    int[] unitSpawn;
    int[] kill;

    void Awake()
    {
        
        instance = this;
        player1UI = GameObject.Find("Panel1");
        player2UI = GameObject.Find("Panel2");
        player1UI.SetActive(false);
        player2UI.SetActive(false);
        
    }
    

    void Update () {
	
	}

    public void mainMenu()
    {
        SceneManager.LoadScene(1);
        GameManager.instance.currentGamestate = GameManager.gameState.Menu;
    }

    public void motherBaseDead(int idLoser)
    {
        GameManager.instance.currentGamestate = GameManager.gameState.Pause;
        Time.timeScale = 0;
        player1UI.SetActive(true);
        player2UI.SetActive(true);
        if (idLoser == 1)
        {
            player1UI.transform.Find("Result").GetComponent<Text>().text = "Defeat";
            player2UI.transform.Find("Result").GetComponent<Text>().text = "Victory";
        }
        else
        {
            player2UI.transform.Find("Result").GetComponent<Text>().text = "Defeat";
            player1UI.transform.Find("Result").GetComponent<Text>().text = "Victory";
        }
        initPlayer(player1UI, 1);
        initPlayer(player2UI, 2);
    }

    void initPlayer(GameObject obj, int id)
    {
        Transform panel = obj.transform.Find("PanelStats");
        panel.Find("Kill").GetComponent<Text>().text += 0;
        panel.Find("Damage").GetComponent<Text>().text += 0;
        panel.Find("APM").GetComponent<Text>().text += 0;
        panel.Find("Unit").GetComponent<Text>().text += 0;
        
    }

}
