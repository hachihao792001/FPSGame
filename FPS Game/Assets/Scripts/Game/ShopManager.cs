using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public string BoughtSound, NotEnoughMoneySound;
    public Transform Items;
    public float upLim, downLim, scrollSpeed;

    public void UpClick()
    {
        Items.position += Vector3.down * scrollSpeed;
        Items.position = new Vector3(Items.position.x, Mathf.Clamp(Items.position.y, downLim, upLim), Items.position.z);
    }

    public void DownClick()
    {
        Items.position += Vector3.up * scrollSpeed;
        Items.position = new Vector3(Items.position.x, Mathf.Clamp(Items.position.y, downLim, upLim), Items.position.z);
    }

    public void HealthOnClick()
    {
        if (FindObjectOfType<MoneyManager>().Money >= 100 && FindObjectOfType<PlayerHealth>().currentHealth < 100)
        {
            FindObjectOfType<MoneyManager>().SendMessage("LostMoney", 100);
            FindObjectOfType<AudioManager>().PlayAudioObj(BoughtSound, FindObjectOfType<GameManager>().FPS.transform).GetComponent<AudioSource>().Play();
            PlayingScript.ActionDidInShop = "BuyFirstAid";
        }
        else FindObjectOfType<AudioManager>().PlayAudioObj(NotEnoughMoneySound, FindObjectOfType<GameManager>().FPS.transform).GetComponent<AudioSource>().Play();
    }

    public void ItemShopOnClick(GameObject item, bool isWeapon, float price)
    {
        MoneyManager MM = FindObjectOfType<MoneyManager>();
        if (MM.Money >= price)
        {
            MM.SendMessage("LostMoney", price);
            FindObjectOfType<AudioManager>().PlayAudioObj(BoughtSound, FindObjectOfType<GameManager>().FPS.transform).GetComponent<AudioSource>().Play();
            GameObject created = Instantiate(item, MM.transform.position + MM.transform.forward.normalized + Vector3.up, Quaternion.identity);
            if (isWeapon)
            {
                PlayingScript.ActionDidInShop = "BuyWeapon";
                PlayingScript.Sender = created;
            }
        }
        else FindObjectOfType<AudioManager>().PlayAudioObj(NotEnoughMoneySound, FindObjectOfType<GameManager>().FPS.transform).GetComponent<AudioSource>().Play();
    }
}
