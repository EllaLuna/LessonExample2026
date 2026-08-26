using UnityEngine;

public class UIManager : MonoBehaviour
{
    PauseMenu pauseMenu;
    void Start()
    {
        pauseMenu = GetComponentInChildren<PauseMenu>(includeInactive: true);
        InputEvents.Pause += OnPause;
    }

    private void OnPause()
    {
        pauseMenu.MenuPressed();
    }

    private void OnDestroy()
    {
        InputEvents.Pause -= OnPause;
    }
}