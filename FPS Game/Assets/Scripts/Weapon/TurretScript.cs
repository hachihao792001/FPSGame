using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public float fullHealth, currentHealth;
    public Transform HealthPivot;

    public Vector3 pickUpPos, pickUpRot;
    public GameObject Bullet, Target;
    public Transform BulletParent, Head, FireBullet;
    public string fireSound;
    public float detectRadius, rate, tick, damage, bulletForce;

    private void Start()
    {
        BulletParent = FindObjectOfType<GameManager>().BulletParent;
        currentHealth = fullHealth;
    }

    void Update()
    {
        if(Target != null)
        {
            Head.LookAt(Target.transform);
            tick += Time.deltaTime;
            if (tick >= rate) {
                GameObject currentBullet = Instantiate(Bullet, BulletParent);
                currentBullet.transform.rotation = FireBullet.transform.rotation;
                currentBullet.SendMessage("setDamage", damage);
                currentBullet.transform.position = FireBullet.transform.position + FireBullet.transform.forward.normalized/2;
                currentBullet.GetComponent<Rigidbody>().AddForce(FireBullet.transform.forward * bulletForce, ForceMode.VelocityChange);
                currentBullet.SendMessage("SendRay", currentBullet.transform.forward);
                tick = 0;

                GameManager.audioM.PlaySound(fireSound, transform, 1f, 20, OptionScreenScript.weaponSound);

            }

            if (Vector3.Distance(transform.position, Target.transform.position) > detectRadius || !Target.GetComponent<Collider>().enabled)
                Target = null;
        }
        else
        {
            GameObject closest = null;
            foreach(Collider c in Physics.OverlapSphere(transform.position, detectRadius))
            {
                if(c.tag == "Enemy")
                {
                    if (closest == null) closest = c.gameObject;
                    else if (Vector3.Distance(transform.position, c.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
                        closest = c.gameObject;
                }
            }
            Target = closest;
        }
    }

    public void Hold(Transform sender)
    {
        FindObjectOfType<HoldScript>().holdingObject = gameObject;

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;

        transform.parent = sender;
        transform.localPosition = pickUpPos;
        transform.localRotation = Quaternion.Euler(pickUpRot);
    }

    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
        transform.parent = null;
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
        transform.position += Vector3.up;
    }

    public void WasAttacked(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        float healthScale = (float)currentHealth / fullHealth;
        HealthPivot.localScale = new Vector3(healthScale, 1, 1);
    }

    public void ShopOnClick()
    {
        FindObjectOfType<ShopManager>().ItemShopOnClick(gameObject, false, 400);
    }
}
