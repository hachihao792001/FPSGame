using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldScript : MonoBehaviour
{
    public bool holdingSomething;
    public GameObject holdingObject;
    public float holdDistance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!holdingSomething)
            {
                RaycastHit rch;
                if (Physics.Raycast(transform.position, transform.forward, out rch, holdDistance))
                {
                    rch.collider.SendMessage("Hold", transform, SendMessageOptions.DontRequireReceiver);
                    if (holdingObject != null) holdingSomething = true;
                }
            }
            else
            {
                holdingObject.SendMessage("Drop");
                holdingObject = null;
                holdingSomething = false;
            }
        }
    }
}
