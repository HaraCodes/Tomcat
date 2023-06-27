using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NWHealthbarScript : NetworkBehaviour
{

    public float MaxHealth;
    [SerializeField] private Image healthBarSprite;

    /*[ClientRpc]
     public void HealthbarUpdateClientRpc()
    {
        int health = NetworkManager.LocalClient.PlayerObject.GetComponentInChildren<HealthScript>().health;
        NetworkManager.LocalClient.PlayerObject.GetComponentInChildren<NWHealthbarScript>().healthBarSprite.fillAmount = health / MaxHealth;
    }*/
    [ServerRpc(RequireOwnership = false)]
    public void HealthbarUpdateServerRpc(float CurrentHealth, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            NWHealthbarScript NWHS = client.PlayerObject.GetComponentInChildren<NWHealthbarScript>();
            NWHS.healthBarSprite.fillAmount = CurrentHealth / MaxHealth;
            int health = client.PlayerObject.GetComponentInChildren<HealthScript>().health.Value;
            NWHS.HealthbarUpdateClientRpc(health);
        }
    }

     [ClientRpc]

     public void HealthbarUpdateClientRpc(float CurrentHealth)
     {
         healthBarSprite.fillAmount = CurrentHealth / MaxHealth;
     }

}