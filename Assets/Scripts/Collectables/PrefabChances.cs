using UnityEngine;

[System.Serializable]
public class PrefabChances
{
    public GameObject prefab;
    [Range(0f, 100f)] public int chance;
}