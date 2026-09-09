using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button backToMenuButton;
    string menuSceneName = "MainMenu";

    void Start()
    {
        continueButton.onClick.AddListener(OnContinueClicked);
        restartButton.onClick.AddListener(OnRestartClicked);
        backToMenuButton.onClick.AddListener(OnMenuClicked);
    }

    private void OnMenuClicked()
    {
        HideMenu();
        SceneManager.LoadScene(menuSceneName);
    }

    private void OnRestartClicked()
    {
        GameStatesEvents.ButtonPressed?.Invoke(GameStateButtonTransition.Restart);
        HideMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnContinueClicked()
    {
        GameStatesEvents.ButtonPressed?.Invoke(GameStateButtonTransition.Continue);
        HideMenu();
    }

    public void MenuPressed()
    {
        if (gameObject.activeSelf)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
    }

    private void HideMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ShowMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
