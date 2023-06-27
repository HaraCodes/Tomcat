using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HbarScript : NetworkBehaviour
{
    MasterHealthScript HealthServerScript;
    public int clientNo;
    [SerializeField] private Image healthBarSprite;
    [SerializeField] float MaxHealth = 150;
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;
        Invoke("DelayRpc", 3);
        HealthServerScript = GameObject.FindGameObjectWithTag("HealthServer").GetComponent<MasterHealthScript>();
        Debug.Log("OnNetwork spawn of Hbar");
    }

    void DelayRpc()
    {
        HealthServerScript.MapServerRpc(clientNo);
    }

    [ClientRpc]
    public void HealthbarUpdateClientRpc(float CurrentHealth)
    {
        healthBarSprite.fillAmount = CurrentHealth / MaxHealth;
    }

}
