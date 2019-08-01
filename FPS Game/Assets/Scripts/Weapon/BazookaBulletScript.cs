using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBulletScript : MonoBehaviour
{
    public GameObject Explosion;
    int damage;
    GameManager gm;
    FPSController FPS;

    void OnEnable()
    {
        StartCoroutine(countDownToSelfExplode());
        gm = FindObjectOfType<GameManager>();
        FPS = FindObjectOfType<FPSController>();
    }

    IEnumerator countDownToSelfExplode()
    {
        yield return new WaitForSeconds(10f);
        ExplosionScript ex = Instantiate(Explosion, transform.position, Quaternion.identity).GetComponent<ExplosionScript>();
        ex.radius *= 3;
        ex.damage *= 3;
        ex.force *= 3;
        ex.transform.localScale *= 3;
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        ExplosionScript ex = Instantiate(Explosion,transform.position, Quaternion.identity).GetComponent<ExplosionScript>();
        ex.radius *= 3;
        ex.damage *= 3;
        ex.force *= 3;
        ex.transform.localScale *= 3;
        Destroy(gameObject);
    }
}
