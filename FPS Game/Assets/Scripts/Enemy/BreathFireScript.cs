using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathFireScript : MonoBehaviour
{
    public float delay, rate, tick, distance;
    public GameObject HorseMenFireBreath;

    void Update()
    {
        tick += Time.deltaTime;
        if (tick >= rate)
        {
            GetComponent<ParticleSystem>().Play();
            StartCoroutine(CreateFire());

            tick = 0;
        }
    }

    IEnumerator CreateFire()
    {
        yield return new WaitForSeconds(delay);
        RaycastHit rch;
        if (Physics.Raycast(transform.position, transform.forward, out rch, distance))
            Instantiate(HorseMenFireBreath, rch.point, Quaternion.identity);
    }
}
