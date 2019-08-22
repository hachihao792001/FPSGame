using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    public Transform PlayerBody,Target;
    public MeshRenderer theMesh;

    private void Awake()
    {
        theMesh = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        PlayerBody = FindObjectOfType<FPSController>().transform.parent;
    }

    void Update()
    {
        transform.LookAt(new Vector3(Target.position.x , transform.position.y, Target.position.z));
        float indiAlpha = 1 - Vector3.Distance(PlayerBody.position, Target.position)/100;
        indiAlpha = Mathf.Clamp(indiAlpha, 0.1f, 1);
        theMesh.material.color = new Color(theMesh.material.color.r, theMesh.material.color.g, theMesh.material.color.b, indiAlpha);

        if (!Target.GetComponent<Collider>().enabled)
            Destroy(gameObject);
    }

    public void SetColor(Color c)
    {
        theMesh.material.color = c;
    }
}
