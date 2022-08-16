using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance instance;
    void Awake () => instance = this;
    
    public int maxHealth = 50;
    [SerializeField] int health = 50;

    [Header("UI")]
    [SerializeField] Transform healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Animator screenFlashDMG;

    void Start () 
    {
        health = maxHealth;
        UpdateHealthBarUI();
    }

    public void TakeDamage (int dmg) 
    {
        health -= dmg;
        screenFlashDMG.SetTrigger("Take Damage");
        UpdateHealthBarUI();
    }

    void UpdateHealthBarUI () 
    {
        healthText.text = health + "/" + maxHealth;
        healthBar.localScale = new Vector3((float)health / (float)maxHealth, 1f, 1f);
    }
}