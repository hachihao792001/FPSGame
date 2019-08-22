using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    int damage;
    GameManager gm;
    FPSController FPS;

    void OnEnable()
    {
        StartCoroutine(countDownToDisappear());
        gm = FindObjectOfType<GameManager>();
        FPS = FindObjectOfType<FPSController>();
    }

    public void SendRay(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, 500f))
        {
            GameObject hitted = hit.collider.gameObject;
            if (hitted.GetComponent<Rigidbody>() != null)
                hitted.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward.normalized * 500f, hit.point);


            if (hitted.tag == "Environment")
            {
                Quaternion holeRotation = Quaternion.LookRotation(hit.normal, Vector3.up);
                holeRotation = Quaternion.Euler(holeRotation.eulerAngles + Vector3.right * 90);

                GameObject hole = Instantiate(gm.BulletHole, hit.point, holeRotation, null);
                hole.transform.localScale = new Vector3(0.1f, 0.0001f, 0.1f);
                hole.transform.parent = hit.collider.transform;
                GameManager.audioM.PlaySound("BulletWall", transform, 1, 5, OptionScreenScript.weaponSound);
            }
            else if (hitted.tag == "Enemy")
            {
                hitted.transform.gameObject.SendMessage("WasAttacked", damage);
            }
            else if (hitted.tag == "EnemyHead")
            {
                hitted.transform.parent.gameObject.SendMessage("WasAttacked", damage * 2);
            }
            else if (hitted.tag == "ExplodingBarrel")
                hitted.SendMessage("BarrelExplode");
        }
    }

    IEnumerator countDownToDisappear()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    public void setDamage(int d)
    {
        damage = d;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
