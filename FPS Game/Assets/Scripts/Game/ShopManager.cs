using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    MoneyManager moneyM;
    AudioManager audioM;
    public string BoughtSound, NotEnoughMoneySound;
    public Transform Items;
    public float upLim, downLim, scrollSpeed;

    private void Start()
    {
        moneyM = FindObjectOfType<MoneyManager>();
        audioM = FindObjectOfType<AudioManager>();
    }

    public void HealthOnClick()
    {
        if (moneyM.Money >= 100 && FindObjectOfType<PlayerHealth>().currentHealth < 100)
        {
            moneyM.SendMessage("LostMoney", 100);
            GameManager.audioM.PlaySound(BoughtSound, transform, 0f, 5, OptionScreenScript.shopSound);
            PlayingScript.ActionDidInShop = "BuyFirstAid";
        }
        else GameManager.audioM.PlaySound(NotEnoughMoneySound, transform, 0f, 5, OptionScreenScript.shopSound);
    }

    public void ArmorOnClick()
    {
        if (moneyM.Money >= 100 && FindObjectOfType<PlayerArmor>().Durability < 100)
        {
            moneyM.SendMessage("LostMoney", 100);
            GameManager.audioM.PlaySound(BoughtSound, transform, 0f, 5, OptionScreenScript.shopSound);
            FindObjectOfType<PlayerArmor>().enabled = true;
            FindObjectOfType<PlayerArmor>().SetDurability(100);
        }
        else GameManager.audioM.PlaySound(NotEnoughMoneySound, transform, 0f, 5, OptionScreenScript.shopSound);
    }

    public void EmergencyOnClick()
    {
        if (moneyM.Money >= 450)
        {
            moneyM.SendMessage("LostMoney", 450);
            GameManager.audioM.PlaySound(BoughtSound, transform, 0f, 5, OptionScreenScript.shopSound);
            PlayingScript.ActionDidInShop = "BuyEmergency";
        }
        else GameManager.audioM.PlaySound(NotEnoughMoneySound, transform, 0f, 5, OptionScreenScript.shopSound);
    }

    public void ItemShopOnClick(GameObject item, bool isWeapon, float price)
    {
        MoneyManager MM = FindObjectOfType<MoneyManager>();
        if (MM.Money >= price)
        {
            MM.SendMessage("LostMoney", price);
            GameManager.audioM.PlaySound(BoughtSound, transform, 0f, 5, OptionScreenScript.shopSound);
            GameObject created = Instantiate(item, MM.transform.position + MM.transform.forward.normalized + Vector3.up, Quaternion.identity);
            if (isWeapon)
            {
                PlayingScript.ActionDidInShop = "BuyWeapon";
                PlayingScript.Sender = created;
            }
        }
        else GameManager.audioM.PlaySound(NotEnoughMoneySound, transform, 0f, 5, OptionScreenScript.shopSound);
    }
}
