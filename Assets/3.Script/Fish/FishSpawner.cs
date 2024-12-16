using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FishSpawner : MonoBehaviour
{
    public Fish.Fish_Direction direction;

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

            int randomIdx;


            randomIdx = Random.Range(0, LevelManager.instance.stageInfo.stageFishes.Length);



            GameObject fishObj = Instantiate(LevelManager.instance.stageInfo.stageFishes[randomIdx],
                transform.position + Vector3.up * Random.Range(-3f, 3f), Quaternion.identity);

            Fish fish = fishObj.GetComponent<Fish>();
            fish.fish_Direction = direction;

            if (fish.fish_Direction == Fish.Fish_Direction.Right)
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z));
            yield return new WaitForSeconds(12.5f + Random.Range(-1.5f, 1.5f));
        }
    }
}