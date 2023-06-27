using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerHolderScript : NetworkBehaviour
{
    //static int count = 0;
    [SerializeField] GameObject cam;
    //[SerializeField] GameObject gun;
    [SerializeField] GameObject PlayerObj;
    //[SerializeField] GameObject HealthMgr;
   // MasterHealthScript HbarServerScript;
    //for syncing Client to corresponding HealthCanvas in HbarScript
    //public int clientNo;

    public override void OnNetworkSpawn()
    {
        
        if (!IsOwner)
        {
            cam.SetActive(false);
      
        }

        if (IsOwner)
        {
            
            PlayerObj.SetActive(false);
            //clientNo = count++;
            
            //Debug.Log("Before Invoke");
            //Invoke("DelayRpc", 3);
            //HbarServerScript.SpawnHealthBarServerRpc(clientNo);
            //Debug.Log("Pholder call to spawnBar over");
        }

     }

    /*void DelayRpc()
    {
        HbarServerScript = GameObject.FindGameObjectWithTag("HealthServer").GetComponent<MasterHealthScript>();
        HbarServerScript.SpawnHealthBarServerRpc(clientNo);
        Debug.Log("Pholder call to spawnBar over");
    }
    */
}
