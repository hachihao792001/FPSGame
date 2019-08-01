using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMarkScript : MonoBehaviour
{
    public float disappearTime, disappearDistance;

    void OnEnable()
    {
        StartCoroutine(CountToDisappear());
    }

    private void Update()
    {
        if (!GameManager.playing) return;
        if (Vector3.Distance(transform.position, FindObjectOfType<GameManager>().FPS.transform.position) >= disappearDistance)
            Destroy(gameObject);
    }

    IEnumerator CountToDisappear()
    {
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }
}
