using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomScript : MonoBehaviour
{
    public float rate, tick, moveSpeed;
    public Vector2 xMinMax, yMinMax, zMinMax;
    public Vector3 randomPos;

    void Start()
    {
        randomPos = new Vector3(
                Random.Range(xMinMax.x, xMinMax.y),
                Random.Range(yMinMax.x, yMinMax.y),
                Random.Range(zMinMax.x, zMinMax.y));
    }

    void Update()
    {
        tick += Time.deltaTime;
        if (tick >= rate)
        {
            randomPos = new Vector3(
                Random.Range(xMinMax.x, xMinMax.y),
                Random.Range(yMinMax.x, yMinMax.y),
                Random.Range(zMinMax.x, zMinMax.y));

            tick = 0;
        }
        GetComponent<Animation>().Play();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.LookAt(randomPos);
        transform.position = Vector3.MoveTowards(transform.position, randomPos, moveSpeed * Time.deltaTime);
    }
}
