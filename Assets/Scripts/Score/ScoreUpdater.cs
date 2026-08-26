using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int score = 0;
    Tween tween;
    Vector3 originalScale;
    float duration = 0.2f;
    float popScale = 1.2f;

    void Start()
    {
        ScoreEvents.UpdateScore += OnUpdateScoreAnimation;
        originalScale = transform.localScale;
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScore(0);
    }

    public void OnUpdateScoreAnimation(int addedScore)
    {
        if (tween != null || tween.IsActive())
        {
            tween.Kill();
        }
        transform.localScale = originalScale;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(originalScale * popScale, duration))
            .SetEase(Ease.InBounce)
            .OnComplete(() => UpdateScore(addedScore))
            .Append(transform.DOScale(originalScale, duration))
            .SetEase(Ease.OutBounce);
        
        tween = sequence;
    }

    private void UpdateScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = $"Score: {score}";
    }

    private void OnDestroy()
    {
        ScoreEvents.UpdateScore -= OnUpdateScoreAnimation;
    }
}
