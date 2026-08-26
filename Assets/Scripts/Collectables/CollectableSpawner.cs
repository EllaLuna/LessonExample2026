using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] Transform markerLeft;
    [SerializeField] Transform markerRight;
    [SerializeField] GameObject prefab;

    float timeToStartSpawn = 1f;
    [SerializeField] float spawnDelay = 0.5f;
    int spawnAmount = 10;

    void Start()
    {
        //StartCoroutine(SpawnOverTime());
        InvokeRepeating(nameof(InfiniteSpawn), timeToStartSpawn, spawnDelay);
    }

    IEnumerator SpawnOverTime()
    {
        yield return new WaitForSeconds(timeToStartSpawn);

        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(prefab, GenerateRandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void InfiniteSpawn()
    {
       var obj = Instantiate(prefab, GenerateRandomPosition(), Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }

    private Vector3 GenerateRandomPosition()
    {
        return new Vector3(Random.Range(markerLeft.position.x, markerRight.position.x), markerRight.position.y, 0);
    }
}
