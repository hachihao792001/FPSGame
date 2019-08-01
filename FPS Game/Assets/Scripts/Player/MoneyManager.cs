using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public GameObject uiPopupText;
    public float Money;
    public Text MoneyText;

    void CreateUIPopupText(string t, Color c)
    {
        GameObject created = Instantiate(uiPopupText);
        created.transform.SetParent(FindObjectOfType<GameManager>().Canvas.transform, false);
        created.SendMessage("SetPos", new Vector3(383, 278, 0));
        created.SendMessage("SetText", t);
        created.SendMessage("SetColor", c);
        created.SendMessage("SetDir", Vector2.down);
    }

    public void GainMoney(float m)
    {
        Money += m;
        CreateUIPopupText("+" + m + "$", Color.green);
        MoneyText.text = Money.ToString() + "$";
    }

    public void LostMoney(float m)
    {
        Money -= m;
        CreateUIPopupText("-" + m + "$", Color.red);
        MoneyText.text = Money.ToString() + "$";
    }
}
