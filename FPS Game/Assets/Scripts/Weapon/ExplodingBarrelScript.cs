using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrelScript : MonoBehaviour
{
    public Vector3 pickUpPos, pickUpRot;
    public bool exploded = false;
    public GameObject Explosion, Fire;

    public void BarrelExplode()
    {
        if (exploded) return;
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Instantiate(Fire, transform.position, Quaternion.identity);
        exploded = true;
        Destroy(gameObject);
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
    }
}
