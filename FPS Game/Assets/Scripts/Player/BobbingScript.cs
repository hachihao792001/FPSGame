using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingScript : MonoBehaviour
{

    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (GetComponent<FPSController>().running)
            GetComponent<Animator>().speed = 2;
        else
            GetComponent<Animator>().speed = 1;

        if (input != Vector3.zero && GetComponent<FPSController>().grounded)
            GetComponent<Animator>().enabled = true;
        else GetComponent<Animator>().enabled = false;
    }
}
