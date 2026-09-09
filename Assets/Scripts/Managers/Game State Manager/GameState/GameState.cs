using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [field: SerializeField] public StateSO stateRules { get; private set; }

    List<TransitionBase> transitions = new();

    void Awake()
    {
        transitions.AddRange(GetComponentsInChildren<TransitionBase>());
    }

    public void Enter()
    {
        transitions.ForEach(x => x.ResetParameters());
        GameStatesEvents.StateUpdated?.Invoke(stateRules);
    }

    public GameState GetNextState()
    {
        foreach (var transition in transitions)
        {
            if (!transition.ShouldTransition())
                continue;

            return transition.TargetState;
        }

        return null;
    }
}