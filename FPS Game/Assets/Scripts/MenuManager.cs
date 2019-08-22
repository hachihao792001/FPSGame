using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text HighScoreText;

    void Start()
    {
        Time.timeScale = 1;
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetFloat("HighScore", 0);

        HighScoreText.text = "High score: " + PlayerPrefs.GetFloat("HighScore").ToString();
    }

    public void PlainOnClick() => SceneManager.LoadScene(1);
    public void SandOnClick() => SceneManager.LoadScene(2);
    public void AbandonedOnClick() => SceneManager.LoadScene(3);
    public void MoutainousOnClick() => SceneManager.LoadScene(4);
    public void ExitOnClick() => Application.Quit();
    public void ClearHighScoreOnClick()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
        HighScoreText.text = "High score: " + PlayerPrefs.GetFloat("HighScore").ToString();
    }

}
