using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    FPSController FPS;
    GameManager gm;
    public float meleeRange, meleeForce;
    public string[] attacks;
    public string hitWallSound, hitEnemySound;
    public GameObject MeleeMark;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        FPS = gm.FPS;

        string name = gameObject.name;
        if (name.Contains("(Clone)"))
            WeaponBarImage = FindObjectOfType<WeaponManager>().GetWeaponBarImage(name.Substring(0, name.Length - 7));
        else
            WeaponBarImage = FindObjectOfType<WeaponManager>().GetWeaponBarImage(name);
    }

    public override void SetBulletNumText()
    {
        FindObjectOfType<WeaponManager>().bulletNum.text = "";
    }

    public override void Attack()
    {
        ani.Stop();
        string currentAttackAni = attacks[Random.Range(0, attacks.Length)];
        ani.Play(currentAttackAni);

        GameManager.audioM.PlaySound(attackSound, transform, 1, 5, OptionScreenScript.weaponSound);

        StartCoroutine(WaitToDealDamage(ani.GetClip(currentAttackAni).length-0.3f));
    }

    IEnumerator WaitToDealDamage(float delay)
    {
        yield return new WaitForSeconds(delay);
        RaycastHit rch;
        if (Physics.Raycast(FPS.transform.position, FPS.transform.forward, out rch, meleeRange))
        {
            GameObject hitted = rch.collider.gameObject;

            if (hitted.tag == "Enemy")
            {
                hitted.gameObject.SendMessage("WasAttacked", damage);
                GameManager.audioM.PlaySound(hitEnemySound, transform, 1, 5, OptionScreenScript.weaponSound);
            }
            else if (hitted.tag == "EnemyHead")
            {
                hitted.transform.parent.gameObject.SendMessage("WasAttacked", damage * 2);
                GameManager.audioM.PlaySound(hitEnemySound, transform, 1, 5, OptionScreenScript.weaponSound);
            }
            else if (hitted.tag == "Environment")
            {
                GameManager.audioM.PlaySound(hitWallSound, transform, 1, 5, OptionScreenScript.weaponSound);
                Quaternion holeRotation = Quaternion.LookRotation(rch.normal, Vector3.up);
                holeRotation = Quaternion.Euler(holeRotation.eulerAngles + Vector3.right * 90);

                GameObject mark = Instantiate(MeleeMark, rch.point, holeRotation, gm.BulletParent);
                mark.transform.parent = hitted.transform;
            }
            else if (hitted.tag == "ExplodingBarrel")
                hitted.SendMessage("BarrelExplode");
                

            Rigidbody hittedRB = hitted.GetComponent<Rigidbody>();
            if (hittedRB != null)
                hittedRB.AddForceAtPosition(FPS.transform.forward.normalized * meleeForce, rch.point);

        }
    }

    public override void Throw()
    {
        transform.parent = null;

        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(FPS.transform.forward * throwForce);
        GetComponent<Rigidbody>().AddTorque(transform.right * throwSpinSpeed);
    }

    public override void PickUp()
    {
        transform.parent = FPS.transform;

        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        transform.localPosition = holdingPos;
        transform.localRotation = Quaternion.Euler(holdingRota);
    }

    public override void ShopOnClick()
    {
        FindObjectOfType<ShopManager>().ItemShopOnClick(gameObject, true, Price);
    }
}
