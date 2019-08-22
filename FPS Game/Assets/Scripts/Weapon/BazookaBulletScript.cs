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
        Physics.IgnoreCollision(GetComponent<Collider>(), FPS.transform.parent.GetComponent<Collider>());
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
        ex.radius *= 2;
        ex.damage *= 2;
        ex.force *= 2;
        ex.transform.localScale *= 2;
        Destroy(gameObject);
    }
}
