using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCOM : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = Vector3.up;
    }
}
