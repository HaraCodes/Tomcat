using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CollisionScript : NetworkBehaviour
{
    
    HealthScript health;
    [SerializeField]
    int damage;
    [SerializeField]
    int headDamage;
    [SerializeField]
    GameObject HealthManager;
    GameObject temp;

    public override void OnNetworkSpawn()
    {
        health = HealthManager.GetComponent<HealthScript>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            health.takeDamage(damage);
            temp = collision.gameObject;
        }
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health.takeDamage(headDamage);
        }
    }*/

    [ServerRpc]
    void DestroyOnHitServerRpc()
    {
        Destroy(temp);
    }
}
