using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScreenScript : MonoBehaviour
{
    public Toggle lightToggle;
    public GameObject Light;
    public Slider backgroundMusic, enemySoundSlider, weaponSoundSlider, shopSoundSlider, allSoundSlider;
    public static float enemySound=1, weaponSound=1, shopSound=1, allSound=1;

    void Start()
    {
        
    }

    public void LightToggleOnOff()
    {
        Light.SetActive(lightToggle.isOn);
    }

    public void SlideBackgroundMusic()
    {
        FindObjectOfType<GameManager>().GetComponent<AudioSource>().volume = Mathf.Min(backgroundMusic.value, allSound);
    }

    public void SlideEnemySound()
    {
        enemySound = enemySoundSlider.value;
    }

    public void SlideWeaponSound()
    {
        weaponSound = weaponSoundSlider.value;
    }

    public void SlideShopSound()
    {
        shopSound = shopSoundSlider.value;
    }

    public void SlideAllSound()
    {
        allSound = allSoundSlider.value;
        FindObjectOfType<GameManager>().GetComponent<AudioSource>().volume = Mathf.Min(backgroundMusic.value, allSound);
    }
}
