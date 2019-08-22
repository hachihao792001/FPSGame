using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendScript : MonoBehaviour
{
    public float fullHealth, currentHealth;
    public Transform HealthPivot;

    public string fireSound;
    Transform PlayerBody;
    public float currentPlayerDistance, maxPlayerDistance, moveSpeed;
    Animator animator;
    public GameObject Bullet, Target;
    public Transform BulletParent, Head, FireBullet;
    public float detectRadius, rate, tick, damage, bulletForce;

    private void Start()
    {
        PlayerBody = FindObjectOfType<PlayerHealth>().transform;
        animator = GetComponent<Animator>();
        BulletParent = FindObjectOfType<GameManager>().BulletParent;
        currentHealth = fullHealth;

    }

    void Update()
    {
        currentPlayerDistance = Vector3.Distance(transform.position, PlayerBody.position);

        if(currentPlayerDistance > maxPlayerDistance)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
            transform.position = Vector3.MoveTowards(transform.position, PlayerBody.position, moveSpeed * Time.deltaTime);
        }
        else GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);


        if (Target != null)
        {
            animator.SetBool("HasTarget", true);
            Head.LookAt(Target.transform);
            transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
            FireBullet.LookAt(Target.transform);

            tick += Time.deltaTime;
            if (tick >= rate)
            {
                GameManager.audioM.PlaySound(fireSound, transform, 1, 20, OptionScreenScript.weaponSound);
                GameObject currentBullet = Instantiate(Bullet, BulletParent);
                currentBullet.transform.rotation = FireBullet.transform.rotation;
                currentBullet.SendMessage("setDamage", damage);
                currentBullet.transform.position = FireBullet.transform.position;
                currentBullet.GetComponent<Rigidbody>().AddForce(FireBullet.transform.forward * bulletForce, ForceMode.VelocityChange);
                currentBullet.SendMessage("SendRay", currentBullet.transform.forward);
                tick = 0;
            }

            if (Vector3.Distance(transform.position, Target.transform.position) > detectRadius || !Target.GetComponent<Collider>().enabled)
                Target = null;
        }
        else
        {
            animator.SetBool("HasTarget", false);
            transform.LookAt(new Vector3(PlayerBody.position.x, transform.position.y, PlayerBody.position.z));

            GameObject closest = null;
            foreach (Collider c in Physics.OverlapSphere(transform.position, detectRadius))
            {
                if (c.tag == "Enemy")
                {
                    if (closest == null) closest = c.gameObject;
                    else if (Vector3.Distance(transform.position, c.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
                        closest = c.gameObject;
                }
            }
            Target = closest;
        }

        animator.SetFloat("PlayerDistance", currentPlayerDistance);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FriendIdle"))
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
    }

    public void WasAttacked(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        float healthScale = (float)currentHealth / fullHealth;
        HealthPivot.localScale = new Vector3(healthScale, 1, 1);
    }

    public void ShopOnClick()
    {
        FindObjectOfType<ShopManager>().ItemShopOnClick(gameObject, false, 500);
    }
}
