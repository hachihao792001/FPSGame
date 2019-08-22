using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public GameObject uiPopupText;
    public float totalMoneyEarned, Money;
    public Text MoneyText;

    private void Start()
    {
        totalMoneyEarned = 0;
        Money = 0;
    }

    void CreateUIPopupText(string t, Color c)
    {
        GameObject created = Instantiate(uiPopupText);
        if (created != null)
            created.transform.SetParent(FindObjectOfType<GameManager>().Canvas.transform, false);
        if (created != null)
            created.SendMessage("SetPos", new Vector3(383, 278, 0));
        if (created != null)
            created.SendMessage("SetText", t);
        if (created != null)
            created.SendMessage("SetColor", c);
        if (created != null)
            created.SendMessage("SetDir", Vector2.down);
    }

    public void GainMoney(float m)
    {
        Money += m;
        CreateUIPopupText("+" + m + "$", Color.green);
        MoneyText.text = Money.ToString() + "$";
        totalMoneyEarned += m;
    }

    public void LostMoney(float m)
    {
        Money -= m;
        CreateUIPopupText("-" + m + "$", Color.red);
        MoneyText.text = Money.ToString() + "$";
    }
}
