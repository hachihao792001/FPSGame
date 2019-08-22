using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Weapon
{
    FPSController FPS;
    GameManager gm;
    public float handRange, handForce;
    public string[] attacks;
    public string hitWallSound, hitEnemySound;
    public GameObject HandMark;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        FPS = gm.FPS;
    }

    public override void SetBulletNumText()
    {
        FindObjectOfType<WeaponManager>().bulletNum.text = "";
    }

    public override void Attack()
    {
        ani.Stop();
        string currentAttackAni = attacks[Random.Range(0, attacks.Length)];
        ani.Play(currentAttackAni);

        GameManager.audioM.PlaySound(attackSound, transform, 1f, 5, OptionScreenScript.weaponSound);

        StartCoroutine(WaitToDealDamage(ani.GetClip(currentAttackAni).length - 0.3f));
    }

    IEnumerator WaitToDealDamage(float delay)
    {
        yield return new WaitForSeconds(delay);
        RaycastHit rch;
        if (Physics.Raycast(FPS.transform.position, FPS.transform.forward, out rch, handRange))
        {
            GameObject hitted = rch.collider.gameObject;

            if (hitted.tag == "Enemy")
            {
                hitted.gameObject.SendMessage("WasAttacked", damage);
                GameManager.audioM.PlaySound(hitEnemySound, transform, 1f, 5, OptionScreenScript.weaponSound);
            }
            else if (hitted.tag == "EnemyHead")
            {
                hitted.transform.parent.gameObject.SendMessage("WasAttacked", damage * 2);
                GameManager.audioM.PlaySound(hitEnemySound, transform, 1f, 5, OptionScreenScript.weaponSound);
            }
            else if (rch.collider.gameObject.tag == "Environment")
            {
                GameManager.audioM.PlaySound(hitWallSound, transform, 1f, 5, OptionScreenScript.weaponSound);

                Quaternion holeRotation = Quaternion.LookRotation(rch.normal, Vector3.up);
                holeRotation = Quaternion.Euler(holeRotation.eulerAngles + Vector3.right * 90);

                GameObject mark = Instantiate(HandMark, rch.point, holeRotation, gm.BulletParent);
                mark.transform.parent = hitted.transform;
            }

            Rigidbody hittedRB = hitted.GetComponent<Rigidbody>();
            if (hittedRB != null)
                hittedRB.AddForceAtPosition(FPS.transform.forward.normalized * handForce, rch.point);
        }
    }

    public override void Throw() { }
    public override void PickUp() { }
    public override void ShopOnClick() { }
        
}
