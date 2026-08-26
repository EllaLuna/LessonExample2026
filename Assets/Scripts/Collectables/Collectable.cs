using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] Color collectedColor;
    [SerializeField] Color failedColor;

    [SerializeField] float delayForDestruction = 1f;
    [SerializeField] float graceTime = 3f;
    SpriteRenderer sprite;
    public bool queuedForDestruction = false;
    bool startedGraceTime = false;
    int score = 1;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (queuedForDestruction)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            queuedForDestruction = true;
            ScoreEvents.UpdateScore?.Invoke(score);
            sprite.color = collectedColor;
            Destroy(gameObject, delayForDestruction);
            return;
        }
        if (other.gameObject.CompareTag("Ground") && !startedGraceTime)
        {
            StartCoroutine(GraceTime());
        }
    }

    IEnumerator GraceTime()
    {
        startedGraceTime = true;
        yield return new WaitForSeconds(graceTime);
        if (queuedForDestruction)
            yield break;
        queuedForDestruction = true;
        sprite.color = failedColor;
        Destroy(gameObject, delayForDestruction);
    }
}
