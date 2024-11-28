using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FishSpawner : MonoBehaviour
{
    public Fish.Fish_Direction direction; 

    static int fish1cnt;
    static int fish2cnt;
    static int fish3cnt;

    private void Start()
    {
        StartCoroutine(SpawnFish());
    }

    IEnumerator SpawnFish()
    {
        while (true)
        {
            yield return new WaitForSeconds(12.5f + Random.Range(-1.5f, 1.5f));

            int randomIdx;


            randomIdx = Random.Range(0, LevelManager.instance.stageInfo.stageFishes.Length);
            //if (randomIdx <= 2)
            //    fish1cnt++;
            //else if (randomIdx == 3)
            //    fish2cnt++;
            //else
            //    fish3cnt++;

            //do
            //{

            //    randomIdx = Random.Range(0, LevelManager.instance.stageInfo.stageFishes.Length);
            //    if (randomIdx <= 2)
            //        fish1cnt++;
            //    else if (randomIdx == 3)
            //        fish2cnt++;
            //    else
            //        fish3cnt++;

            //    if (fish1cnt < 6 && fish2cnt < 3 && fish3cnt < 3)
            //        break;

            //    if (randomIdx <= 2)
            //        fish1cnt--;
            //    else if (randomIdx == 3)
            //        fish2cnt--;
            //    else
            //        fish3cnt--;

            //    yield return null;
            //} while (fish1cnt >= 6 && fish2cnt >= 3 && fish3cnt >= 3);

            if (!LevelManager.instance.isPausedGame)
            {
                GameObject fishObj = Instantiate(LevelManager.instance.stageInfo.stageFishes[randomIdx],
                    transform.position + Vector3.up * Random.Range(-3f, 3f), Quaternion.identity);

                Fish fish = fishObj.GetComponent<Fish>();
                fish.fish_Direction = direction;
                 
                if (fish.fish_Direction == Fish.Fish_Direction.Right)
                    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
                        transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z));
            }
        }
    }
}