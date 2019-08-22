using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearBeamScript : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.localScale = new Vector3(2, 500, 2);
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
