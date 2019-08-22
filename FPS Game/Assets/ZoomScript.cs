using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour
{
    public float ZoomFOV, NormalFOV;
    public GameObject ZoomScreen;
    Camera c;

    private void Start()
    {
        c = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ZoomScreen.SetActive(true);
            c.fieldOfView = ZoomFOV;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            ZoomScreen.SetActive(false);
            c.fieldOfView = NormalFOV;
        }
    }
}
