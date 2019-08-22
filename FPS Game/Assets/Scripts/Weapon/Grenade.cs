using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Weapon
{
    public GameObject Explosion;
    GameManager gm;
    public float grenadeThrowForce;
    public string thrownAni;
    public GameObject explodeEffect;
    public float explodeRadius, explodeForce;
    bool thrown = false, exploded = false;
    FPSController FPS;
    public float shakeDuration, shakeMagnitude;
    public GameObject explodedMark;

    public string throwGrenade, explode;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        FPS = FindObjectOfType<FPSController>();
        ani = GetComponent<Animation>();

        string name = gameObject.name;
        if (name.Contains("(Clone)"))
            WeaponBarImage = FindObjectOfType<WeaponManager>().GetWeaponBarImage(name.Substring(0, name.Length - 7));
        else
            WeaponBarImage = FindObjectOfType<WeaponManager>().GetWeaponBarImage(name);

        Physics.IgnoreCollision(GetComponent<Collider>(), FPS.transform.parent.GetComponent<Collider>());
    }

    public override void Attack()
    {
        ani.Play(aniAttack);
        StartCoroutine(CountToThrow(ani.GetClip(aniAttack).length));
    }

    IEnumerator CountToThrow(float s)
    {
        yield return new WaitForSeconds(s);
        transform.parent = null;
        ani.Play(thrownAni);
        GetComponent<Rigidbody>().isKinematic = false;
        transform.GetChild(0).GetComponent<Collider>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().AddForce(gm.FPS.transform.forward * grenadeThrowForce);
        GameManager.audioM.PlaySound(throwGrenade, transform, 1, 5, OptionScreenScript.weaponSound);
        thrown = true;
        FindObjectOfType<WeaponManager>().weaponInSlot[4] = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thrown && !exploded)
        {
            //StartCoroutine(Explode());
            ExplosionScript e = Instantiate(Explosion, transform.position, Quaternion.identity).GetComponent<ExplosionScript>();
            e.damage = damage;
            e.radius = explodeRadius;
            e.force = explodeForce;
            e.shakeDuration = shakeDuration;
            e.shakeMagnitude = shakeMagnitude;
            exploded = true;
        }
    }

    float Distance(Transform a, Transform b)
    {
        return (a.position - b.position).magnitude;
    }

    /*
    IEnumerator Explode()
    {
        explodeEffect.SetActive(true);
        Instantiate(explodedMark, transform.position+Vector3.up*0.1f, Quaternion.identity);

        Vector3 origin = FPS.transform.localPosition;
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(shakeDuration, shakeMagnitude));
        FPS.transform.localPosition = origin;

        Collider[] hits = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach(Collider c in hits)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null && rb.isKinematic == false)
            {
                GameObject hitted = c.gameObject;
				
                if (hitted.tag == "Enemy") {
					hitted.SendMessage("SetHitByExplosion", true);
					hitted.SendMessage("WasAttacked", Mathf.Round(damage * 3 - (Distance(hitted.transform, transform)/explodeRadius)*damage*3));
                }else if (hitted.tag == "Player") hitted.SendMessage("WasAttacked", Mathf.Round(damage - (Distance(hitted.transform, transform) / explodeRadius) * damage));
                else if (hitted.tag == "ExplodingBarrel") hitted.SendMessage("BarrelExplode");

                if (hitted.tag != "Mark")
                    rb.AddExplosionForce(explodeForce, transform.position, explodeRadius);
            }
        }
        GameManager.audioM.PlaySound(explode, transform, 0.5f, 5);

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    */

    public override void PickUp()
    {
        transform.parent = FPS.transform;

        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        transform.localPosition = holdingPos;
        transform.localRotation = Quaternion.Euler(holdingRota);
    }

    public override void Throw()
    {
        transform.parent = null;

        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(FPS.transform.forward * throwForce);
        GetComponent<Rigidbody>().AddTorque(transform.forward * throwSpinSpeed);
    }

    public override void SetBulletNumText()
    {
        FindObjectOfType<WeaponManager>().bulletNum.text = "";
    }

    public override void ShopOnClick()
    {
        FindObjectOfType<ShopManager>().ItemShopOnClick(gameObject, true, Price);
    }
}
