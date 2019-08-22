using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidObj : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        GameObject hit = collision.collider.gameObject;

        if (hit.tag == "Player" && hit.GetComponent<PlayerHealth>().currentHealth < 100)
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(PickedUp(hit));
        }
    }

    IEnumerator PickedUp(GameObject hit)
    {
        hit.SendMessage("GainHealth", 5);
        GameManager.audioM.PlaySound("FirstAidPickUp", transform, 1, 5, 1);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
