using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearAfterSeconds : MonoBehaviour
{
    public float seconds;
    void Start()
    {
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
