using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Pause, PauseScreen, ShopScreen, OptionScreen, Playing, PopupText, Canvas, Indicator;
    public WeaponManager wm;

    public FPSController FPS;
    public static AudioManager audioM;

    public Transform Crossair;

    public static bool playing = true, shooting = false, dead = false;

    public GameObject BulletHole;
    public Transform BulletParent;

    private void Awake()
    {
        audioM = FindObjectOfType<AudioManager>();
        dead = false;
    }

    private void Start()
    {
        playing = true;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !dead)
        {
            Pause.SetActive(!Pause.activeSelf);
            playing = !playing;
            Time.timeScale = (Time.timeScale==0?1:0);

            if (Pause.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                PauseScreen.SetActive(true);
                ShopScreen.SetActive(false);
                OptionScreen.SetActive(false);
                Playing.SetActive(false);
                foreach(GameObject g in GameObject.FindGameObjectsWithTag("AudioObj"))
                    g.GetComponent<AudioSource>().Pause();
                
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                Playing.SetActive(true);
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("AudioObj"))
                {
                    if (g.GetComponent<AudioSource>().clip.name != "Cash" && g.GetComponent<AudioSource>().clip.name != "Wrong")
                        g.GetComponent<AudioSource>().Play();
                }
            }
        }

        if (dead)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
	
	public void ReplayOnClick(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuOnClick()
    {
        SceneManager.LoadScene(0);
    }
}
