using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    Collider[] thingsInSide;
    public float damage, radius, rate, duration;
    public string fireSound;
    float tick = 0;

    private void OnEnable()
    {
        StartCoroutine(CountDownToDisppear(duration));
        transform.rotation = Quaternion.identity;
        GameObject audio = GameManager.audioM.PlayAudioObj(fireSound, transform);
        audio.GetComponent<AudioSource>().spatialBlend = 0.5f;
        audio.GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (!GameManager.playing) return;
        tick += Time.deltaTime;

        if (tick >= rate)
        {
            thingsInSide = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider c in thingsInSide)
            {
                GameObject thing = c.gameObject;
                if (thing.name!="HorseMen" && (thing.tag == "Enemy" || thing.tag == "Player" || thing.transform.Find("PlayerSide") != null))
                {
                    thing.SendMessage("WasAttacked", damage, SendMessageOptions.RequireReceiver);
                }
                else if (thing.tag == "ExplodingBarrel") thing.SendMessage("BarrelExplode");

            }
            tick = 0;
        }
    }

    IEnumerator CountDownToDisppear(float s)
    {
        yield return new WaitForSeconds(s);
        //GameManager.audioM.GetSound(fireSound).Stop();
        Destroy(gameObject);
    }
}
