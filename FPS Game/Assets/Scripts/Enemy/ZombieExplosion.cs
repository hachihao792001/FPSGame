﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieExplosion : MonoBehaviour
{
    public float radius, damage, force, timeToDisappear;
    public string sound;

    float Distance(Transform a, Transform b)
    {
        return (a.position - b.position).magnitude;
    }

    private void OnEnable()
    {
        GameObject audio = GameManager.audioM.PlayAudioObj(sound, transform);
        audio.GetComponent<AudioSource>().spatialBlend = 0.5f;
        audio.GetComponent<AudioSource>().Play();

        foreach (Collider c in Physics.OverlapSphere(transform.position, radius))
        {
            GameObject hit = c.gameObject;
            hit.SendMessage("WasAttacked", Mathf.Round(damage - (Distance(hit.transform, transform) / radius) * damage), SendMessageOptions.DontRequireReceiver);
            hit.SendMessage("BarrelExplode", SendMessageOptions.DontRequireReceiver);
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) rb.AddExplosionForce(force, transform.position, radius);
        }
        StartCoroutine(CountDownToDisappear());
    }

    IEnumerator CountDownToDisappear()
    {
        yield return new WaitForSeconds(timeToDisappear);
        Destroy(gameObject);
    }
}