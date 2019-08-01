using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    WeaponManager wm;
    Weapon currentWeapon;
    public float tick;

    // Start is called before the first frame update
    void Start()
    {
        wm = FindObjectOfType<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.playing) return;
        if (!GameManager.playing) return;
        currentWeapon = wm.weaponInSlot[wm.currentSlot];
        tick += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (currentWeapon != null)
            {
                if (tick >= currentWeapon.rate)
                {
                    currentWeapon.Attack();
                    tick = 0;
                }
            }
        }
    }
}
