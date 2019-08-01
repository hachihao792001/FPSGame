using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreenScript : MonoBehaviour
{
    Image img;
    private void OnEnable()
    {
        img = GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
    }

    private void Update()
    {
        if (!GameManager.playing) return;
        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a-0.05f);
        if (img.color.a <= 0) gameObject.SetActive(false);
    }
}
