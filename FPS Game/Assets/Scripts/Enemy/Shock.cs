using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : MonoBehaviour
{
    public float radius, shockForce, shockDamage;
    public float shakeDuration, shakeMagnitude;

    private void OnEnable()
    {
        FPSController FPS = FindObjectOfType<FPSController>();
        Vector3 origin = FPS.transform.localPosition;
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(shakeDuration, shakeMagnitude));
        FPS.transform.localPosition = origin;

        foreach (Collider c in Physics.OverlapSphere(transform.position, radius))
        {
            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(shockForce, transform.position, radius*2);
                c.gameObject.SendMessage("WasAttacked", shockDamage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
