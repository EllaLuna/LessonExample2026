using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] Transform markerLeft;
    [SerializeField] Transform markerRight;
    [SerializeField] List<PrefabChances> prefabChances;
    float timeToStartSpawn = 1f;
    [SerializeField] float spawnDelay = 0.5f;
    int spawnAmount = 10;

    const int maxChance = 100;
    void Start()
    {
        prefabChances = prefabChances.OrderBy(x => x.chance).ToList();
        //StartCoroutine(SpawnOverTime());
        InvokeRepeating(nameof(SpawnRandomPrefab), timeToStartSpawn, spawnDelay);
    }

    IEnumerator SpawnOverTime()
    {
        yield return new WaitForSeconds(timeToStartSpawn);

        for (int i = 0; i < spawnAmount; i++)
        {

            SpawnRandomPrefab();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnRandomPrefab()
    {
        Instantiate(ChooseRandomPrefab(), GenerateRandomPosition(), Quaternion.identity);
    }

    private GameObject ChooseRandomPrefab()
    {
        int random = Random.Range(0, maxChance);
        GameObject chosenPrefab = prefabChances.Last().prefab;
        foreach (var prefab in prefabChances)
        {
            if (random <= prefab.chance)
            {
                chosenPrefab = prefab.prefab;
                break;
            }
        }
        return chosenPrefab;
    }

    private Vector3 GenerateRandomPosition()
    {
        return new Vector3(Random.Range(markerLeft.position.x, markerRight.position.x), markerRight.position.y, 0);
    }
}