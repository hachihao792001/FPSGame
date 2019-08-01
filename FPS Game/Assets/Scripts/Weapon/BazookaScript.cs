using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaScript : Weapon
{
    public GameObject BazookaBullet;
    Transform BulletParent;
    public int bulletForce;
    FPSController FPS;

    public int currentBulletNum, totalBullet;

    public int magazine;
    public string reloadSound;

    void Start()
    {
        currentBulletNum = magazine;
        FPS = FindObjectOfType<GameManager>().FPS;
        BulletParent = FindObjectOfType<GameManager>().BulletParent;

        string name = gameObject.name;
        if (name.Contains("(Clone)"))
        {
            WeaponBarImage = FindObjectOfType<WeaponManager>().GetWeaponBarImage(name.Substring(0, name.Length - 7));
        }
        else
            WeaponBarImage = FindObjectOfType<WeaponManager>().GetWeaponBarImage(name);

    }

    private void OnEnable()
    {
        SetBulletNumText();
    }

    void Update()
    {
        if (!GameManager.playing) return;
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    public override void SetBulletNumText()
    {
        FindObjectOfType<WeaponManager>().bulletNum.text = currentBulletNum + "/" + totalBullet;
    }

    public override void Attack()
    {
        if (ani.IsPlaying(aniReload)) return;
        if (currentBulletNum > 0)
        {
            GameManager.shooting = true;
            ani.Stop(aniAttack);
            ani.Play(aniAttack);

            GameManager.audioM.PlayAudioObj(attackSound, transform).GetComponent<AudioSource>().Play();

            GameObject currentBullet = Instantiate(BazookaBullet, BulletParent);
            currentBullet.transform.rotation = FPS.transform.rotation;
            currentBullet.transform.position = FPS.transform.position + FPS.transform.forward.normalized*2;
            currentBullet.GetComponent<Rigidbody>().AddForce(FPS.shootDirection.transform.forward * bulletForce, ForceMode.VelocityChange);

            currentBulletNum--;
        }

        SetBulletNumText();
        StartCoroutine(CountToShootFalse());
    }

    IEnumerator CountToShootFalse()
    {
        yield return new WaitForSeconds(0.3f);
        GameManager.shooting = false;

    }

    public void Reload()
    {
        if (ani.IsPlaying(aniReload) || transform.parent == null) return;
        if (totalBullet > 0 && currentBulletNum < magazine)
        {
            ani.Stop();
            ani.Play(aniReload);
            GameManager.audioM.PlayAudioObj(reloadSound, transform).GetComponent<AudioSource>().Play(); ;

            StartCoroutine(AfterReload());
        }
    }

    IEnumerator AfterReload()
    {
        yield return new WaitForSeconds(ani.GetClip(aniReload).length);
        int firedBullet = magazine - currentBulletNum;
        if (totalBullet >= firedBullet)
        {
            currentBulletNum += firedBullet;
            totalBullet -= firedBullet;
        }
        else
        {
            currentBulletNum += totalBullet;
            totalBullet = 0;
        }

        SetBulletNumText();
    }

    public override void Throw()
    {
        transform.parent = null;

        GetComponent<BoxCollider>().enabled = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            Collider c = transform.GetChild(i).GetComponent<Collider>();
            if (c != null)
                c.enabled = true;
        }
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(FPS.transform.forward * throwForce);
        GetComponent<Rigidbody>().AddTorque(transform.forward * throwSpinSpeed);
    }

    public override void PickUp()
    {
        transform.parent = FPS.transform;

        GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            Collider c = transform.GetChild(i).GetComponent<Collider>();
            if (c != null)
                c.enabled = false;
        }

        GetComponent<Rigidbody>().isKinematic = true;

        transform.localPosition = holdingPos;
        transform.localRotation = Quaternion.Euler(holdingRota);
    }

    public override void ShopOnClick()
    {
        FindObjectOfType<ShopManager>().ItemShopOnClick(gameObject, true, Price);
    }
}
