using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] CollectableSO data;
    SpriteRenderer sprite;
    public bool queuedForDestruction = false;
    bool startedGraceTime = false;

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
            ScoreEvents.UpdateScore?.Invoke(data.Score);
            sprite.color = data.CollectedColor;
            Destroy(gameObject, data.DelayForDestruction);
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
        yield return new WaitForSeconds(data.GraceTime);
        if (queuedForDestruction)
            yield break;
        queuedForDestruction = true;
        sprite.color = data.FailedColor;
        Destroy(gameObject, data.DelayForDestruction);
    }
}
