using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MasterHealthScript : NetworkBehaviour
{
    //spawn a health bar
    [SerializeField] 
    GameObject HealthCanvasPrefab;
    static ulong[] map = new ulong[4] ;
    [ServerRpc(RequireOwnership =false)]
    public void SpawnHealthBarServerRpc(int count, ServerRpcParams serverRpcParams= default)
     {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            Transform clientTransform = client.PlayerObject.GetComponentInChildren<HealthBarPos>().CanvasPos;
            GameObject hbar = Instantiate(HealthCanvasPrefab, clientTransform.position, clientTransform.rotation);
            hbar.GetComponent<HbarScript>().clientNo = count;
            hbar.GetComponent<NetworkObject>().Spawn();
            Debug.Log("Successfully spawned hbar using ServerRpc");
        }
     }

    //pair player client id with this healthbar clientid
    [ServerRpc(RequireOwnership = false)]
    public void MapServerRpc(int clientNo, ServerRpcParams serverRpcParams= default)
    {
        map[clientNo] = serverRpcParams.Receive.SenderClientId;
    }
    
    
    [ServerRpc]
    public void HealthbarUpdateServerRpc(float health, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            //int clientNo = client.PlayerObject.GetComponent<PlayerHolderScript>().clientNo;
            //var healthCanvas = NetworkManager.ConnectedClients[map[clientNo]];
            //healthCanvas.PlayerObject.GetComponent<HbarScript>().HealthbarUpdateClientRpc(health);
        }
    }

}
