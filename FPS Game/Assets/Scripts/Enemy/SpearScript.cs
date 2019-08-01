using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    public float damage, damageRate, disappearTime;
    float tick;
    bool hittedSomething = false;

    private void Start()
    {
        StartCoroutine(CountToDisappear());
    }

    private void Update()
    {
        if (hittedSomething)
        {
            tick += Time.deltaTime;
            if (tick >= damageRate)
            {
                transform.parent.SendMessage("WasAttacked", damage);
                tick = 0;

                if (!transform.parent.GetComponent<Collider>().enabled)
                    hittedSomething = false;
            }
        }
    }

    IEnumerator CountToDisappear()
    {
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Spear") return;
        collision.collider.SendMessage("BarrelExplode", SendMessageOptions.DontRequireReceiver);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        GetComponent<Rigidbody>().isKinematic = true;

        Transform hit = collision.collider.transform;
        transform.parent = hit;

        if (!hittedSomething)
        {
            if (hit.tag == "Player" || hit.tag == "Enemy" || hit.tag == "EnemyHead" || hit.Find("PlayerSide")!=null)
                hittedSomething = true;
        }
    }
}
