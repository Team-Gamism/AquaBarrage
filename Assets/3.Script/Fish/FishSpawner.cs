using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public Fish.Fish_Direction direction;

    public StageFishSO stageFish;

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
            yield return new WaitForSeconds(7.5f + Random.Range(-1.5f, 1.5f));

            int randomIdx;


            randomIdx = Random.Range(0, stageFish.stageFishes.Length);
            if (randomIdx <= 2)
                fish1cnt++;
            else if (randomIdx == 3)
                fish2cnt++;
            else
                fish3cnt++;

            while (fish1cnt >= 5 && fish2cnt >= 2 && fish3cnt >= 2)
            {
                if (randomIdx <= 2)
                    fish1cnt--;
                else if (randomIdx == 3)
                    fish2cnt--;
                else
                    fish3cnt--;

                randomIdx = Random.Range(0, stageFish.stageFishes.Length);
                if (randomIdx <= 2)
                    fish1cnt++;
                else if (randomIdx == 3)
                    fish2cnt++;
                else
                    fish3cnt++;
            } 

            GameObject fishObj = Instantiate(stageFish.stageFishes[randomIdx], transform.position + Vector3.up * Random.Range(-3f, 3f), Quaternion.identity);
            
            Fish fish = fishObj.GetComponent<Fish>();
            fish.fish_Direction = direction;

            if (fish.fish_Direction == Fish.Fish_Direction.right)
                transform.rotation = Quaternion.Euler( new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z));
        }
    }
}