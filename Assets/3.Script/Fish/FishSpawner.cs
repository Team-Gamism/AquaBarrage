using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FishSpawner : MonoBehaviour
{
    public Fish.Fish_Direction direction;
    [SerializeField] FishDataBase fishDataBase;

    private void Start()
    {
        StartCoroutine(SpawnFish());
    }

    IEnumerator SpawnFish()
    {
        while (true)
        {
            if (GameManager.Instance.isClearStage)
                break;

            GameObject prefab = GetFishPrefab();

            GameObject fishObj = Instantiate(prefab,
                transform.position + Vector3.up * Random.Range(-3f, 3f), Quaternion.identity);

            Fish fish = fishObj.GetComponent<Fish>();
            fish.fish_Direction = direction;

            if (fish.fish_Direction == Fish.Fish_Direction.Right)
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z));
            yield return new WaitForSeconds(12.5f + Random.Range(-1.5f, 1.5f));
        }
    }

    GameObject GetFishPrefab()
    {
        var stageFishList = LevelManager.instance.stageInfo.fishSpawnInfoList;

        FishType fishType =
            GetRandomByWeight(stageFishList);

        var data = fishDataBase.Get(fishType.ToString());
        if (data == null)
        {
            Debug.LogError($"FishData not found: {fishType}");
            return null;
        }

        GameObject prefab =
            AddressableManager.Instance.Get<GameObject>(data.prefabAddress);

        if (prefab == null)
        {
            Debug.LogError($"Prefab not loaded: {data.prefabAddress}");
        }

        return prefab;
    }

    public static FishType GetRandomByWeight(List<FishSpawnInfo> list)
    {
        float total = 0f;
        foreach (var info in list)
            total += info.probability;

        float rand = Random.Range(0f, total);
        float current = 0f;

        foreach (var info in list)
        {
            current += info.probability;
            if (rand <= current)
                return info.fishType;
        }

        // fallback (ÀÌ·Ð»ó ¾È Å½)
        return list[0].fishType;
    }
}