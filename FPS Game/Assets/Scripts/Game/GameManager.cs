using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Pause, PauseScreen, ShopScreen,Playing, PopupText, Canvas;
    public WeaponManager wm;

    public FPSController FPS;
    public static AudioManager audioM;

    public Transform Crossair;

    private void Awake()
    {
        audioM = FindObjectOfType<AudioManager>();
        dead = false;
    }

    public static bool playing = true, shooting = false, dead = false;

    public GameObject BulletHole;
    public Transform BulletParent;

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
                PauseScreen.SetActive(true);
                ShopScreen.SetActive(false);
                Playing.SetActive(false);
                foreach(GameObject g in GameObject.FindGameObjectsWithTag("AudioObj"))
                    g.GetComponent<AudioSource>().Pause();
                
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Playing.SetActive(true);
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("AudioObj"))
                    g.GetComponent<AudioSource>().Play();
            }
        }

        if (dead) Cursor.lockState = CursorLockMode.None;
    }
	
	public void ReplayOnClick(){
		SceneManager.LoadScene(0);
	}
}
