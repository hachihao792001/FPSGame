using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    public Vector3 pickUpPos, pickUpRot;

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

    public void ShopOnClick()
    {
        FindObjectOfType<ShopManager>().ItemShopOnClick(gameObject, false, 10);
    }
}
