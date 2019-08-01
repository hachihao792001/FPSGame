using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyObj : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().AddTorque(Vector3.one * 500);
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject hit = collision.collider.gameObject;

        if (hit.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(PickedUp(hit));
        }
    }

    IEnumerator PickedUp(GameObject hit)
    {
        hit.SendMessage("GainMoney", 10);
        GameManager.audioM.PlayAudioObj("MoneyPickUp", transform).GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
