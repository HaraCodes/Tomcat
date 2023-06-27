using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerObjectScript : NetworkBehaviour
{
    //Master server;
    public NetworkVariable<int> health = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner) ;
    
    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        health.Value = 100;
    }
    public TMP_Text t;
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

        Debug.Log("Spawned client requested for a health manager");

        reqHealthChangeClientRpc(damage, clientRpcParams);
        //spawnHealthBarClientRpc(count, clientId);

    }
    [ClientRpc]
    public void reqHealthChangeClientRpc(int damage, ClientRpcParams clientRpcParams)
    {
        PlayerObjectScript ps = GetComponent<PlayerObjectScript>();
        ps.health.Value -= damage;
        
    }

    /*public override void OnNetworkSpawn()
    {
        server = GameObject.FindGameObjectWithTag("HealthServer").GetComponent<Master>();
    }*/

    void Update()
    {
        t.text = health.Value.ToString();
        if (!IsOwner)
            return;
        if (Input.GetKey("t"))
        {
            healthServerRpc(5);
        }
    }
    
}
