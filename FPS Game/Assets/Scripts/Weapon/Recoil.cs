using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    FPSController FPS;
    Transform Crossair;
    Transform dir;
    public float recoil, recoilIncrease, recoilDecrease, recoilLim, damp;

    private void Start()
    {
        FPS = FindObjectOfType<GameManager>().FPS;
        Crossair = FindObjectOfType<GameManager>().Crossair;
        dir = FPS.shootDirection.transform;
        recoil = recoilIncrease;
    }

    private void Update()
    {
        if (!GameManager.playing) return;
        if (recoil > 0) recoil -= recoilDecrease;
        else recoil = 0;

        dir.rotation = Quaternion.Slerp(dir.rotation, FPS.transform.rotation, damp);

        Crossair.localScale = Vector3.Lerp(Crossair.localScale, (Vector3.one * (recoil + 1)), damp);
        //Debug.Log((Vector3.one * (recoil + 1)) / 2);
    }

    public void Shoot()
    {
        dir.Rotate(Random.Range(-recoil, recoil), Random.Range(-recoil, recoil), 0);
        FPS.yaw -= Random.Range(-recoil / 5, recoil/5);
        FPS.pitch += Random.Range(-recoil / 5, recoil / 5);



        if (recoil < recoilLim)
            recoil += recoilIncrease;

    }
}
