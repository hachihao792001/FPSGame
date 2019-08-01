using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public Image[] weaponBarSlots = new Image[5];
    public GameObject[] weaponBarWeaponImages;

    public Text bulletNum;

    //0 1 2 3 4
    public int currentSlot;
    public Weapon[] weaponInSlot = new Weapon[5];

    public float pickUpDistance;

    Attack attack;

    void Start()
    {
        attack = GetComponent<Attack>();
        currentSlot = 0;
        ChangeToSlot(0);
    }

    public GameObject GetWeaponBarImage(string name)
    {
        foreach (GameObject w in weaponBarWeaponImages)
            if (w.name == name) return w;
        return null;
    }

    public void ChangeToSlot(int slot)
    {
        if (weaponInSlot[slot] == null) return;
        if ((weaponInSlot[currentSlot] != null && !weaponInSlot[currentSlot].ani.isPlaying)
        || weaponInSlot[currentSlot] == null)
        {
            attack.tick = 0;
            for (int i = 0; i < weaponInSlot.Length; i++)
            {
                if (i != slot && weaponInSlot[i] != null)
                {
                    weaponInSlot[i].ani.Stop();
                    //GameManager.audioM.GetSound(weaponInSlot[i].attackSound).Stop();
                    //if (i <= 2 && i>0)
                     //   GameManager.audioM.GetSound(weaponInSlot[i].ownGunScript.reloadSound).Stop();
                }
            }

            currentSlot = slot;
            for (int i = 0; i < weaponInSlot.Length; i++)
            {
                if (weaponInSlot[i] != null)
                    weaponInSlot[i].gameObject.SetActive(i == slot);
            }

            for (int i = 0; i < weaponBarSlots.Length; i++)
                if (i == slot) weaponBarSlots[i].color = Color.yellow;
                else weaponBarSlots[i].color = Color.white;
            weaponInSlot[slot].SendMessage("SetBulletNumText");
        }
    }

    void Update()
    {
        if (!GameManager.playing) return;
        foreach (GameObject img in weaponBarWeaponImages)
            img.SetActive(false);
        foreach(Weapon w in weaponInSlot)
        {
            if (w != null)
            {
                w.WeaponBarImage.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            ChangeToSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeToSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeToSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeToSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeToSlot(4);
        }

        if (Input.GetKeyDown(KeyCode.G) && currentSlot != 0)
        {
            if (weaponInSlot[currentSlot] != null)
            {
                if (!weaponInSlot[currentSlot].ani.isPlaying)
                {
                    weaponInSlot[currentSlot].SendMessage("Throw");
                    weaponInSlot[currentSlot] = null;
                    bulletNum.text = "";
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if ((weaponInSlot[currentSlot] != null && !weaponInSlot[currentSlot].ani.isPlaying)
            || weaponInSlot[currentSlot] == null)
            {
                RaycastHit rch;
                if (Physics.Raycast(transform.position, transform.forward, out rch, pickUpDistance))
                {
                    GameObject hitted = rch.collider.gameObject;
                    if (hitted.tag == "Weapon" && weaponInSlot[hitted.GetComponent<Weapon>().type] ==null)
                    {
                        hitted.SendMessage("PickUp");
                        weaponInSlot[hitted.GetComponent<Weapon>().type] = hitted.GetComponent<Weapon>();
                        ChangeToSlot(hitted.GetComponent<Weapon>().type);
                    }
                }
            }
        }
    }

    public void PlayerDead()
    {
        for (int i = 1; i < weaponInSlot.Length; i++)
        {
            if (weaponInSlot[i] != null)
            {
                weaponInSlot[i].transform.parent = null;
                weaponInSlot[i].gameObject.SetActive(true);
                weaponInSlot[i].GetComponent<Collider>().enabled = true;
                weaponInSlot[i].GetComponent<Rigidbody>().isKinematic = false;
                weaponInSlot[i].GetComponent<Rigidbody>().AddExplosionForce(300, transform.position + Vector3.down, 3);
                weaponInSlot[i] = null;
            }
        }
    }
}
