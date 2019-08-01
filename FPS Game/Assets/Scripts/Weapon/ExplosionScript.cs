using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    FPSController FPS;
    public float radius, damage, force;
    public float shakeDuration, shakeMagnitude;
    public string sound;

    float Distance(Transform a, Transform b)
    {
        return (a.position - b.position).magnitude;
    }

    private void OnEnable()
    {
        StartCoroutine(EXPLODE());
    }

    IEnumerator EXPLODE()
    {
        yield return new WaitForSeconds(0.1f);
        FPS = FindObjectOfType<FPSController>();

        GameObject audio = GameManager.audioM.PlayAudioObj(sound, transform);
        audio.GetComponent<AudioSource>().spatialBlend = 0.5f;
        audio.GetComponent<AudioSource>().Play();

        Vector3 origin = FPS.transform.localPosition;
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(shakeDuration, shakeMagnitude));
        FPS.transform.localPosition = origin;

        foreach (Collider c in Physics.OverlapSphere(transform.position, radius))
        {
            GameObject hit = c.gameObject;
			if(hit.tag == "Enemy") hit.SendMessage("SetHitByExplosion", true);
            hit.SendMessage("WasAttacked", Mathf.Round(damage - (Distance(hit.transform, transform) / radius) / 4 * damage), SendMessageOptions.DontRequireReceiver);
			if (hit.tag == "ExplodingBarrel") StartCoroutine(TriggerAnotherBarrel(hit));
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) rb.AddExplosionForce(force, transform.position, radius);
        }
    }

    IEnumerator TriggerAnotherBarrel(GameObject hit)
    {
        yield return new WaitForSeconds(1f);
        if(hit!=null)
        hit.SendMessage("BarrelExplode");
    }
}
