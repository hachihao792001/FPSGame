using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupTextScript : MonoBehaviour
{
    public Text tx;
    public Vector2 dir;
    public float moveSpeed, fadeSpeed;

    private void Start()
    {
        tx = GetComponent<Text>();
    }

    public void SetPos(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public void SetText(string t)
    {
        tx.text = t;
    }

    public void SetColor(Color c)
    {
        tx.color = c;
    }

    public void SetDir(Vector2 d)
    {
        dir = d;
    }

    void Update()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
        tx.color = new Color(tx.color.r, tx.color.g, tx.color.b, tx.color.a - fadeSpeed * Time.deltaTime);
        if (tx.color.a <= 0) Destroy(gameObject);
    }
}
