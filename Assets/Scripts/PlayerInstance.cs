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
    public int gold = 0;
    [SerializeField] Sprite goldPic;

    [Header("UI")]
    [SerializeField] Transform healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Animator screenFlashDMG;
    [SerializeField] TextMeshProUGUI subtitleInfoText;
    [SerializeField] TextMeshProUGUI goldText;

    [Header("Blocking")]
    public BlockingWeapon currentBlockingWeapon;
    public string currentBlocking;

    CinemachineShake cs;

    void Start () 
    {
        health = maxHealth;
        UpdateHealthBarUI();
        cs = CinemachineShake.instance;
        UpdateGoldText();
    }

    public void TakeDamage (int dmg) 
    {
        health -= dmg;
        screenFlashDMG.SetTrigger("Take Damage");
        UpdateHealthBarUI();
        cs.ShakeCamera(12f, 0.3f, 0.05f, 90);
    }

    void UpdateHealthBarUI () 
    {
        healthText.text = health + "/" + maxHealth;
        healthBar.localScale = new Vector3((float)health / (float)maxHealth, 1f, 1f);
    }

    public void AddGold (int amt) 
    {
        gold += amt;
        print("Player given " + gold + " gold.");
        InventoryManager.instance.TriggerPickupNotif(goldPic, "+" + amt + " gold");
        UpdateGoldText();
    }

    void UpdateGoldText () => goldText.text = gold.ToString();

    public void DeductGold (int amt) 
    {
        gold -= amt;
        UpdateGoldText();
    }

    public void ShowInfoText (string t) 
    {
        subtitleInfoText.gameObject.SetActive(true);
        subtitleInfoText.text = t;
    }

    public void HideInfoText () 
    {
        subtitleInfoText.gameObject.SetActive(false);
    }
}