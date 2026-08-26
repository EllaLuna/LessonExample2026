using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button startButton;

    TextMeshProUGUI textMeshPro;
    string firstLevelName = "Level1";


    void Start()
    {
        startButton.onClick.AddListener(() => StartGame());
    }

    private void StartGame()
    {
        Debug.Log("Start game");
        SceneManager.LoadScene(firstLevelName);
    }

    public void ExitGame()
    {
        Debug.Log("Game Exiting");
        Application.Quit();
    }
}
