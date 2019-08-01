using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float Price;
    public Gun ownGunScript;
    public Melee ownMeleeScript;
    public Grenade ownGrenadeScript;
    public GameObject WeaponBarImage;
    public int type;
    public Vector3 holdingPos, holdingRota;
    public int damage;
    public float rate;
    public Animation ani;
    public string attackSound;
    public string aniAttack, aniReload;
    public int throwForce, throwSpinSpeed;

    public abstract void Attack();
    public abstract void Throw();
    public abstract void PickUp();
    public abstract void SetBulletNumText();
    public abstract void ShopOnClick();

    protected MoneyManager MM;
    private void Start()
    {
        MM = FindObjectOfType<MoneyManager>();
    }
}
