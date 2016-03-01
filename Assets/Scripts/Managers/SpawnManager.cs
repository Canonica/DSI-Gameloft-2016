using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnManager : NetworkBehaviour {

    NetworkStartPosition[] spawns;
    int spawnerUsed = 0;
    // Use this for initialization
    void OnLevelWasLoaded(int level)
    {
        if (isServer && level == 2)
        {
            spawns = GameObject.Find("SpawnHolder").GetComponentsInChildren<NetworkStartPosition>();
            int cmpt = 1;
            while (spawns.Length > cmpt)
            {
                spawns[cmpt].gameObject.SetActive(false);
                cmpt++;
            }
        }
        else
        {
            if(level == 2)
            CmdPlayerConnect();
        }
	}

    [Command]
    public void CmdPlayerConnect()
    {
            Debug.Log("CLIENT CONNECT");
            if (spawns[spawnerUsed] != null)
                spawns[spawnerUsed].gameObject.SetActive(false);
            spawnerUsed++;
            if (spawns[spawnerUsed] != null)
                spawns[spawnerUsed].gameObject.SetActive(true);
    }
}
