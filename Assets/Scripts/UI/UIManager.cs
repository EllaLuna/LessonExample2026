using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    PauseMenu pauseMenu;
    void Start()
    {
        pauseMenu = GetComponentInChildren<PauseMenu>(includeInactive: true);
        GameStatesEvents.StateUpdated += OnStateUpdated;
    }

    private void OnStateUpdated(StateSO stateSo)
    {
        if(stateSo.CanMenu)
            pauseMenu.MenuPressed();
    }

    private void OnDestroy()
    {
        GameStatesEvents.StateUpdated -= OnStateUpdated;
    }
}