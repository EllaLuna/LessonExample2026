using UnityEngine;

[CreateAssetMenu(fileName = "CollectableSO", menuName = "Scriptable Objects/CollectableSO", order = 0)]
public class CollectableSO : ScriptableObject
{
    [Header("Score")]
    [SerializeField] int score = 1;
    public int Score { get => score; private set => score = value; }

    [Header("Timing")]
    [SerializeField] float delayForDestruction = 1f;
    public float DelayForDestruction { get => delayForDestruction; private set => delayForDestruction = value; }
    [SerializeField] float graceTime = 3f;
    public float GraceTime { get => graceTime; private set => graceTime = value; }

    [Header("Colors")]
    [SerializeField] Color collectedColor;
    public Color CollectedColor { get => collectedColor; private set => collectedColor = value; }
    [SerializeField] Color failedColor;
    public Color FailedColor { get => failedColor; private set => failedColor = value; }
}
