using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;
using System.Net;
using System.Net.Sockets;

public class MenuManager : MonoBehaviour
{
    public NetworkManager manager;
    public static MenuManager instance = null;

    public GameObject MainMenuCanvas;
    public GameObject CreditsCanvas;
    public GameObject OptionsCanvas;
    public GameObject ConnectCanvas;

    public GameObject PauseCanvas;

    public CanvasGroup MainMenuCanvasGroup;
    public CanvasGroup CreditsCanvasGroup;
    public CanvasGroup OptionsCanvasGroup;
    public CanvasGroup ConnectCanvasGroup;
    Toggle isHost;
    InputField ipAdress;
    public float fadeInSpeed;
    public float fadeOutSpeed;

    float delay = 0.5f;


    void Awake()
    {
        if (instance == null)
            instance = this;
        
    }

    void Start()
    {
        manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        isHost = null;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            MainMenuCanvas = GameObject.Find("MainMenuCanvas");
            CreditsCanvas = GameObject.Find("CreditsCanvas");
            OptionsCanvas = GameObject.Find("OptionsCanvas");
            ConnectCanvas = GameObject.Find("ConnectCanvas");

            MainMenuCanvasGroup = MainMenuCanvas.GetComponent<CanvasGroup>();
            CreditsCanvasGroup = CreditsCanvas.GetComponent<CanvasGroup>();
            OptionsCanvasGroup = OptionsCanvas.GetComponent<CanvasGroup>();
            ConnectCanvasGroup = ConnectCanvas.GetComponent<CanvasGroup>();

            foreach (Transform child in MainMenuCanvas.transform)
            {
                switch (child.name)
                {
                    case "PlayButton":
                        child.GetComponent<Button>().onClick.AddListener(() => { ShowConnect(); });
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
           
            ConnectCanvas.transform.FindChild("ConnectButton").GetComponent<Button>().onClick.AddListener(() => { Play(); });
            ConnectCanvas.transform.FindChild("MainMenuButton").GetComponent<Button>().onClick.AddListener(() => { Main_Menu_From_Connect();});
            ConnectCanvas.transform.FindChild("HostCheck").GetComponent<Toggle>().onValueChanged.AddListener((val) => {ToggleHost(val);});

            if (isHost == null) isHost = GameObject.Find("HostCheck").GetComponent<Toggle>();
            if (ipAdress == null) ipAdress = GameObject.Find("AdressText").GetComponent<InputField>();
            isHost.isOn = false;
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
        if (checkIsHost())
        {
            manager.networkAddress = "localhost";
            Debug.Log("HOST");
            GameManager.GetInstance().currentGamestate = GameManager.gameState.Waiting;
            manager.StartHost();
        }
        else
        {
            
            manager.networkAddress = ipAdress.text;
            Debug.Log("CLIENT " + manager.networkAddress);
            GameManager.GetInstance().currentGamestate = GameManager.gameState.Playing;
            manager.StartClient();
            Invoke("isConnected", 2f);
        }

        //
        //SceneManager.LoadScene(2);
    }

    void isConnected()
    {
        if (!manager.client.isConnected)
        {
            Debug.Log("fail");

        }
    }

    void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
    }

    public void ToggleHost(bool val)
    {
        ipAdress.interactable = !val;
        if (val)
        {
            ipAdress.text = LocalIPAddress();
        }
        else
        {
            ipAdress.text = "";
        }
    }

    public string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
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

    public void Main_Menu_From_Connect()
    {
        HideAll();
        MainMenuCanvas.SetActive(true);
        StartCoroutine(fadeIn(MainMenuCanvasGroup, fadeInSpeed));
        StartCoroutine(fadeOut(ConnectCanvasGroup));

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

    public void ShowConnect()
    {
        HideAll();
        ConnectCanvas.SetActive(true);
        StartCoroutine(fadeIn(ConnectCanvasGroup, fadeInSpeed));
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

    bool checkIsHost()
    {
        
        return isHost.isOn;
    }



}