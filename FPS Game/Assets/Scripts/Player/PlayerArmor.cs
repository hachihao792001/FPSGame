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
        Durability = 0;
        ArmorText.gameObject.SetActive(false);  
        ArmorText.text = "0";
    }

    public void DecreaseDurability(float d)
    {
        Durability -= d;
        ArmorText.text = Durability.ToString();
        if (Durability > 0) ArmorText.gameObject.SetActive(true);
    }

    public void SetDurability(float d)
    {
        Durability = d;
        ArmorText.text = Durability.ToString();
        if(Durability>0) ArmorText.gameObject.SetActive(true);
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
