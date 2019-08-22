using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotate : MonoBehaviour
{
    public float speed;
    void Update()
    {
        if(GameManager.playing)
            transform.Rotate(new Vector3(0, speed, 0), Space.World);
    }
}
