using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRain : MonoBehaviour
{
    public GameObject MoneyObj;
    public float height, xMin, zMin, xMax, zMax, rate, tick=0;

    void Update()
    {
        tick += Time.deltaTime;
        if (tick >= rate)
        {
            Instantiate(MoneyObj, new Vector3(Random.Range(xMin, xMax), height, Random.Range(zMin, zMax)), Quaternion.identity);
            tick = 0;
        }
    }
}
