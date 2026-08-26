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
        gameObject.SetActive(false);
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
        HideMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnContinueClicked()
    {
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
