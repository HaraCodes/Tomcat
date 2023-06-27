using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : NetworkBehaviour
{
    public NetworkVariable<int> health = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    //public NetworkVariable<int> fillamt = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public int maxhealth;
    private HealthbarScript hBar;
    //MasterHealthScript hBar2;
    GameObject healthManagerClient;
    public GameObject Foreground;
    private Image healthBarSprite;

    bool isTom = true;
    bool isDead = false;

    [Header("Death Animation")]
    public GameObject armature;
    Animator animator;
    int IsDeadHash;

    //collision Script
    [SerializeField]
    int damage;
    [SerializeField]
    int headDamage;

    public void OnHealthChanged(int previousValue, int newValue)
    {
        if(newValue <= 0)
        {
            newValue = 0;
        }
        healthBarSprite.fillAmount = (float)newValue / (float)maxhealth;
    }

    //Collision Script
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(damage);
            //temp = collision.gameObject;
        }
    }
    public override void OnNetworkSpawn()
    {
        if (isTom)
            maxhealth = 150;
        else
            maxhealth = 100;

        

        //Death Animation
        animator = armature.GetComponent<Animator>();
        IsDeadHash = Animator.StringToHash("IsDead");

        //Health bar above head
        healthBarSprite = Foreground.GetComponent<Image>();
        if(IsOwner)
        health.Value = maxhealth;
        health.OnValueChanged += OnHealthChanged;
        OnHealthChanged(0, health.Value);
        
        //Health bar overlay
        healthManagerClient = GameObject.FindGameObjectWithTag("ClientHealthMgr");
        hBar = healthManagerClient.GetComponent<HealthbarScript>();
        hBar.MaxHealth = maxhealth;
        //hBar2 = GameObject.FindGameObjectWithTag("HealthServer").GetComponent<MasterHealthScript>();
    }

    
    public void takeDamage(int damage)
    {
        if (!IsOwner) return;
        health.Value -= damage;
        if (health.Value <= 0)
        {
            isDead = true;
            animator.SetBool(IsDeadHash, true);

            hBar.HealthbarUpdate(0);
            Debug.Log(health.Value);
            //hBar2.HealthbarUpdateServerRpc(0f);
        }
        else 
        {
            
            hBar.HealthbarUpdate(health.Value);
            Debug.Log(health.Value);
            //hBar2.HealthbarUpdateServerRpc(health);

        }
    }

    void Update()
    {
        if (isDead)
        {
            //play death animatio
            //despawn after few seconds
            //enable spectating
        }
    }
}
