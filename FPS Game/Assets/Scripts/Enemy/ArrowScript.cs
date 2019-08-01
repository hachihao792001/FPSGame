using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float damage, disappearTime;
    bool hittedSomething = false;

    private void Start()
    {
        StartCoroutine(CountToDisappear());
    }

    IEnumerator CountToDisappear()
    {
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Arrow") return;

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;

        Transform hit = collision.collider.transform;
        transform.parent = hit;

        if (!hittedSomething)
        {
            if (hit.tag == "EnemyHead")
            {
                hit.parent.SendMessage("WasAttacked", damage * 2);
                hittedSomething = true;
            }
            else hit.SendMessage("WasAttacked", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
