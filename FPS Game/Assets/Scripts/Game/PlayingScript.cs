using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingScript : MonoBehaviour
{
    public WeaponManager wm;
    public static GameObject Sender;
    public static string ActionDidInShop = "";

    private void Start()
    {
        wm = FindObjectOfType<WeaponManager>();
    }

    private void OnEnable()
    {
        StartCoroutine(DoStuff());
    }

    IEnumerator DoStuff()
    {
        yield return new WaitForSeconds(0.1f);
        if (ActionDidInShop == "BuyFirstAid")
            FindObjectOfType<PlayerHealth>().SendMessage("GainHealth", 10);
        else if (ActionDidInShop == "BuyWeapon")
        {
            if (wm.weaponInSlot[Sender.GetComponent<Weapon>().type] == null)
            {
                Sender.SendMessage("PickUp");
                wm.weaponInSlot[Sender.GetComponent<Weapon>().type] = Sender.GetComponent<Weapon>();
                wm.ChangeToSlot(Sender.GetComponent<Weapon>().type);
            }
        }else if(ActionDidInShop == "BuyEmergency")
        {
            FindObjectOfType<EmergencyScript>().doIt = true;
        }

        Sender = null;
        ActionDidInShop = "";
    }
}
