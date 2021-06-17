using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text settingsText;
    public GameObject heroGameObject;
    public GameObject menu;
    public Text scoreText;
    public Text livesText;
    public int heroLives = 3;
    public Button resumeButton;

    private Hero heroScript;
    private int score = 0;

    private void Awake()
    {
        heroScript = heroGameObject.GetComponent<Hero>();
        if (scoreText != null)
            scoreText.text = $"Счёт {score}";
    }

    private void Update()
    {
        Pause();
        SettingsText();
        LivesText();
    }

    public void ResumeGame()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
        if (Time.timeScale != 1)
            Time.timeScale = 1;
    }
    public void ChangeSettings()
    {
        if (!heroScript.keyboardControl)
            heroScript.keyboardControl = true;
        else
            heroScript.keyboardControl = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeInHierarchy == false)
            {
                menu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                menu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void Score(int sc)
    {
        score += sc;
        scoreText.text = $"Счёт {score}";
    }

    private void SettingsText()
    {
        settingsText.text = heroScript.keyboardControl ? "Управление: клавиатура" : "Управление: клавиатура + мышь";
    }

    private void LivesText()
    {
        if (livesText != null)
            livesText.text = $"Жизней {heroLives}";
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        resumeButton.interactable = false;
        menu.SetActive(true);
    }
}
