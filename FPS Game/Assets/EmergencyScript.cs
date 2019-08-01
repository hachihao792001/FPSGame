using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyScript : MonoBehaviour
{
    public FPSController FPS;
    public GameObject Effect;
    public float FlyForce, healPoint, pushForce;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            ShopOnClick();
    }

    public void ShopOnClick()
    {
        StartCoroutine(DoEffectStuff());
        foreach(Collider c in Physics.OverlapSphere(transform.position, 10f))
        {
            if (c.GetComponent<Rigidbody>() != null && c.tag != "Player")
                c.GetComponent<Rigidbody>().AddExplosionForce(2000, transform.position, 10);
        }
        GetComponent<Rigidbody>().AddForce(Vector3.up * FlyForce);
        GetComponent<PlayerHealth>().GainHealth(healPoint);
        GetComponent<PlayerArmor>().SetDurability(100);

    }

    IEnumerator DoEffectStuff()
    {
        Effect.SetActive(true);
        FPS.runSpeed = 20;
        yield return new WaitForSeconds(5f);
        Effect.SetActive(false);
        FPS.runSpeed = 7;

    }
}
