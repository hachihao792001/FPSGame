using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Zombie, SmallZombie;
    public float rate, min, max, ySpawn;
    public Vector2 xArea, zArea;
    float tick;

    void Update()
    {
        if (!GameManager.playing) return;
        tick += Time.deltaTime;
        if (tick > rate)
        {
            float currentNum = Random.Range(min, max + 1);
            for (int i=0; i< currentNum/2; i++)
            {
                GameObject a = Instantiate(Zombie,
                    new Vector3(Random.Range(xArea.x, xArea.y), ySpawn, Random.Range(zArea.x, zArea.y)), 
                    Quaternion.identity);
            }
            for (int i = 0; i < currentNum / 2; i++)
            {
                GameObject a = Instantiate(SmallZombie,
                    new Vector3(Random.Range(xArea.x, xArea.y), ySpawn, Random.Range(zArea.x, zArea.y)),
                    Quaternion.identity);
            }
            tick = 0;
        }
    }
}
