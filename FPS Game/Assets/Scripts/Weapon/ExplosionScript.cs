using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public string[] ExplodeSounds;
    public GameObject[] ExplodeEffects;

    FPSController FPS;
    public GameObject Mark;
    public float radius, damage, force;
    public float shakeDuration, shakeMagnitude;

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

        RaycastHit rch;
        Physics.Raycast(transform.position + Vector3.up*6, Vector3.down, out rch);
        Quaternion holeRotation = Quaternion.LookRotation(rch.normal, Vector3.up);
        holeRotation = Quaternion.Euler(holeRotation.eulerAngles + Vector3.right * 90);
        GameObject mark = Instantiate(Mark, rch.point, holeRotation, null);
        mark.transform.position += mark.transform.up*0.2f;
        mark.transform.parent = rch.collider.transform;

        ExplodeEffects[Random.Range(0, ExplodeEffects.Length)].GetComponent<ParticleSystem>().Play();

        GameManager.audioM.PlaySound(ExplodeSounds[Random.Range(0, ExplodeSounds.Length)], transform, 0.5f, 30, OptionScreenScript.weaponSound);

        Vector3 origin = FPS.transform.localPosition;
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(shakeDuration, shakeMagnitude));
        FPS.transform.localPosition = origin;

        foreach (Collider c in Physics.OverlapSphere(transform.position, radius))
        {
            GameObject hit = c.gameObject;
            if (hit.tag == "Enemy")
            {
                hit.SendMessage("SetHitByExplosion", true);
                hit.SendMessage("WasAttacked", Mathf.Round(damage - (Distance(hit.transform, transform) / radius) / 4 * damage) * 2, SendMessageOptions.DontRequireReceiver);
            }else
                hit.SendMessage("WasAttacked", Mathf.Round(damage - (Distance(hit.transform, transform) / radius) / 4 * damage), SendMessageOptions.DontRequireReceiver);

            if (hit.tag == "ExplodingBarrel") StartCoroutine(TriggerAnotherBarrel(hit));
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && hit.tag != "Mark") rb.AddExplosionForce(force, transform.position, radius);
        }
    }

    IEnumerator TriggerAnotherBarrel(GameObject hit)
    {
        yield return new WaitForSeconds(1f);
        if(hit!=null)
        hit.SendMessage("BarrelExplode");
    }
}
