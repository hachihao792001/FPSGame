using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    PlayerArmor playerArmor;
    public GameObject uiPopupText;
    public float fullHealth, currentHealth;
    FPSController FPS;
    public Text playerHeathText;
    public GameObject deadScreen, damageScreen;

    private void Start()
    {
        playerArmor = GetComponent<PlayerArmor>();
        FPS = FindObjectOfType<FPSController>();
        currentHealth = fullHealth;
        playerHeathText.text = fullHealth.ToString();
    }

    void CreateUIPopupText(string t, Color c)
    {
        GameObject created = Instantiate(uiPopupText);
        created.transform.SetParent(FindObjectOfType<GameManager>().Playing.transform, false);
        created.SendMessage("SetPos", new Vector3(383, -276, 0));
        created.SendMessage("SetText", t);
        created.SendMessage("SetColor", c);
        created.SendMessage("SetDir", Vector2.up);
    }

    public void WasAttacked(float damage)
    {
        damageScreen.SetActive(true);

        if (currentHealth > 0)
        {
            if (playerArmor.enabled)
            {
                SendMessage("DecreaseDurability", damage);
                damage = Mathf.Round(damage / 3);
            }

            currentHealth -= damage;
            CreateUIPopupText("-"+damage.ToString(), Color.red);
            currentHealth = Mathf.Clamp(currentHealth, 0, fullHealth);
            playerHeathText.text = currentHealth.ToString();

            if (currentHealth <= 0)
            {
                deadScreen.SetActive(true);

                FPS.GetComponent<Attack>().enabled = false;
                FPS.GetComponent<CameraShake>().enabled = false;
                FPS.GetComponent<WeaponManager>().enabled = false;
                FPS.enabled = false;
                GameManager.dead = true;

                FPS.SendMessage("PlayerDead");
            }
        }
    }

    public void GainHealth(float health)
    {
        currentHealth += health;
        CreateUIPopupText("+" + health.ToString(), Color.green);
        currentHealth = Mathf.Clamp(currentHealth, 0, fullHealth);
        playerHeathText.text = currentHealth.ToString();
    }
}
