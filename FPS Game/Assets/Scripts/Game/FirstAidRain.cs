using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidRain : MonoBehaviour
{
    public GameObject FirstAidObj;
    public float height, xMin, zMin, xMax, zMax, rate, tick = 0;

    void Update()
    {
        tick += Time.deltaTime;
        if (tick >= rate)
        {
            Instantiate(FirstAidObj, new Vector3(Random.Range(xMin, xMax), height, Random.Range(zMin, zMax)), Quaternion.identity);
            tick = 0;
        }
    }
}
