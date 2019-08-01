using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : Weapon
{
    public GameObject Fire;
    public float molThrowForce, timeToDisappearFire;
    GameManager gm;
    FPSController FPS;
    public string throwMol, placeFire;
    bool thrown = false, placedFire = false;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        FPS = FindObjectOfType<FPSController>();
        ani = GetComponent<Animation>();

        string name = gameObject.name;
        if (name.Contains("(Clone)"))
            WeaponBarImage = FindObjectOfType<WeaponManager>().GetWeaponBarImage(name.Substring(0, name.Length - 7));
        else
            WeaponBarImage = FindObjectOfType<WeaponManager>().GetWeaponBarImage(name);
    }

    public override void Attack()
    {
        ani.Play(aniAttack);
        StartCoroutine(CountToThrow(ani.GetClip(aniAttack).length));
    }

    IEnumerator CountToThrow(float s)
    {
        yield return new WaitForSeconds(s);
        ani.Stop(aniAttack);
        transform.SetParent(null, true);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().AddForce(gm.FPS.transform.forward * molThrowForce);
        GameManager.audioM.PlayAudioObj(throwMol, transform).GetComponent<AudioSource>().Play();
        thrown = true;
        FindObjectOfType<WeaponManager>().weaponInSlot[4] = null;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thrown && !placedFire)
        {
            Instantiate(Fire, transform.position, Quaternion.identity);
            Destroy(gameObject);
            placedFire = true;
        }
    }

    public override void PickUp()
    {
        transform.parent = FPS.transform;

        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        transform.localPosition = holdingPos;
        transform.localRotation = Quaternion.Euler(holdingRota);
    }

    public override void SetBulletNumText()
    {
        FindObjectOfType<WeaponManager>().bulletNum.text = "";
    }

    public override void Throw()
    {
        transform.parent = null;

        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(FPS.transform.forward * throwForce);
        GetComponent<Rigidbody>().AddTorque(transform.forward * throwSpinSpeed);
    }

    public override void ShopOnClick()
    {
        FindObjectOfType<ShopManager>().ItemShopOnClick(gameObject, true, Price);
    }
}
