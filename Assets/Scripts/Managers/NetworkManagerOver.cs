using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManagerOver : NetworkManager {
    

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        
        
        Debug.Log("new client " + conn.connectionId);
        

        base.OnServerAddPlayer(conn, playerControllerId);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("client disconnected");
        this.StopHost();
        base.OnServerDisconnect(conn);
        
    }
}
