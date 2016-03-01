using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    /*
    Variable toujours mise a jour sur les clients
    [SyncVar]
    public float  myVar;
    */

    // Use this for initialization
    void Start () {
	    
	}

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer) return;

        float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical");
        transform.Translate(h, 0.0f, v);

    }
    /*
    Envoyer aux clients une action (exemple : animation / sound / etc.)

    void Action()
    {
        DoAction;
        CmdTest();
    }

    [Command]
    void CmdTest()
    {
        RpcTest();
    }

    [ClientRpc]
    void RpcTest()
    {
        
    }*/

    /*
    Commande du serveur appelé depuis le client
    
    [Command]
    void CmdDo()
    {
        NetworkServer.Spawn(GameObject); // Spawn un gameobject chez tous les joueurs
    }
    
    Commande du client appelé depuis le serveur
    
    [ClientRpc]
    void RpcDo()
    {
        if (isLocalPlayer)
        {
            
        }
    }
    
    */
}
