using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletScritpt : NetworkBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject bulletPoint;
    [SerializeField]
    float bulletCooldown;



    float speed = 100;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsOwner) return;
            shootServerRpc(bulletPoint.transform.position, bulletPoint.transform.rotation, transform.forward);
        }
    }

    [ServerRpc]
    void shootServerRpc(Vector3 position, Quaternion rotation, Vector3 forward, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        var client = NetworkManager.ConnectedClients[clientId];
        //Debug.Log(client.PlayerObject);
        GameObject bullet = Instantiate(bulletPrefab,position, rotation);
        bullet.GetComponent<NetworkObject>().Spawn();
        bullet.GetComponent<Rigidbody>().AddForce(forward * speed);
        Destroy(bullet, bulletCooldown);
    }
   
}
