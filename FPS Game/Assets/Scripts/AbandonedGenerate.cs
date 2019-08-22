using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbandonedGenerate : MonoBehaviour
{
    public GameObject[] stuffs;

    void Start()
    {
        foreach (GameObject g in stuffs)
            for (int j = 0; j < Random.Range(5, 18); j++)
                Instantiate(g, new Vector3(Random.Range(-140, 140), 0, Random.Range(-140, 140)), Quaternion.identity);
    }
}
