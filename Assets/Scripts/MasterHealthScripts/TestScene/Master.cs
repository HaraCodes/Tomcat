using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Master : NetworkBehaviour
{
    //[SerializeField] GameObject HbarPrefab;
    
    [ServerRpc]
    public void healthServerRpc(int damage, ServerRpcParams serverRpcParams = default)
    {
        ulong clientId = serverRpcParams.Receive.SenderClientId;
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientId }
            }
        };
        if (!IsHost)
            return;
        Debug.Log("Spawned client requested for a health manager");

        reqHealthChangeClientRpc(damage, clientRpcParams);
        //spawnHealthBarClientRpc(count, clientId);

    }
    [ClientRpc]
    public void reqHealthChangeClientRpc(int damage, ClientRpcParams clientRpcParams)
    {
        PlayerObjectScript ps = GetComponent<PlayerObjectScript>();
        ps.health.Value -= damage;
        GetComponent<TextMeshPro>().text = ps.health.ToString();
    }

    
}






/*[ClientRpc]
    public void spawnHealthBarClientRpc(int count, ulong id)
    {
        GameObject hObj = Instantiate(HbarPrefab);
        hObj.transform.position = GetComponent<Transform>().position;
        hObj.GetComponent<NetworkObject>().Spawn();
    }

    [ClientRpc]
    public void setPlayerNoClientRpc(ClientRpcParams clientRpcParams)
    {
        GetComponent<PlayerObjectScript>().playerno = count;
    }*/
