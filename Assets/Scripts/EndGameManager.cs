using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour {
    public static EndGameManager instance = null;
    GameObject player1UI, player2UI;
    public int[] playerDeath;
    public int[] playerDamage;
    public int[] playerSpawn;

    void Awake()
    {
        
        instance = this;
        player1UI = GameObject.Find("Panel1");
        player2UI = GameObject.Find("Panel2");
        player1UI.SetActive(false);
        player2UI.SetActive(false);

        playerDeath = new int[3];
        playerDamage = new int[3];
        playerSpawn = new int[3];
        playerDeath[1] = 0;
        playerDeath[2] = 0;
        playerDamage[1] = 0;
        playerDamage[2] = 0;
        playerSpawn[1] = 0;
        playerSpawn[2] = 0;
    }
    

    void Update () {
	
	}

    public void mainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        GameManager.instance.currentGamestate = GameManager.gameState.Menu;
    }

    public void motherBaseDead(int idLoser)
    {
        XInput.instance.useVibe(1, 0, 0, 0);
        XInput.instance.useVibe(2, 0, 0, 0);
        GameManager.instance.currentGamestate = GameManager.gameState.Menu;
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
        panel.Find("Kill").GetComponent<Text>().text += playerDeath[id%2+1];
        panel.Find("Damage").GetComponent<Text>().text += playerDamage[id];
        panel.Find("APM").GetComponent<Text>().text ="APM : "+ Random.Range(10,400);
        panel.Find("Unit").GetComponent<Text>().text += playerSpawn[id];
        
    }

    public void addDeath(int id)
    {
        playerDeath[id] ++;
    }

    public void addSpawn(int id, int value=1)
    {
        playerSpawn[id] += value;
    }

    public void addDamage(int id, int value = 1)
    {
        playerDamage[id] += value;
    }

}
