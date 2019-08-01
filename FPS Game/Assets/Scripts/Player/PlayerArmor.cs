using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArmor : MonoBehaviour
{
    public float Durability;
    public Text ArmorText; 

    private void OnEnable()
    {
        Durability = 100;
        ArmorText.gameObject.SetActive(true);
        ArmorText.text = "100";
    }

    public void DecreaseDurability(float d)
    {
        Durability -= d;
        ArmorText.text = Durability.ToString();
    }

    public void SetDurability(float d)
    {
        Durability = d;
        ArmorText.text = Durability.ToString();
    }

    void Update()
    {
        if (Durability <= 0)
        {
            ArmorText.gameObject.SetActive(false);
            GetComponent<PlayerArmor>().enabled = false;
        }
    }
}
