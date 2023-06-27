using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript :  MonoBehaviour
{

    public float MaxHealth;
    [SerializeField]private Image healthBarSprite;
    public void HealthbarUpdate(int CurrentHealth)
    {
        healthBarSprite.fillAmount = CurrentHealth / MaxHealth;
    }
}
