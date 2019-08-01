using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextScript : MonoBehaviour
{
    public TextMesh tm;
    public float moveUpSpeed, fadeSpeed;

    public void SetText(string t)
    {
        tm.text = t;
    }

    public void SetColor(Color c)
    {
        tm.color = c;
    }

    void Update()
    {
        transform.forward = transform.position - FindObjectOfType<GameManager>().FPS.transform.position;
        transform.Translate(Vector3.up * moveUpSpeed * Time.deltaTime);
        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, tm.color.a - fadeSpeed * Time.deltaTime);
        if (tm.color.a <= 0) Destroy(gameObject);
    }
}
