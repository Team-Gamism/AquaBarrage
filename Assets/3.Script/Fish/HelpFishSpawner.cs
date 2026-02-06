using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpFishSpawner : MonoBehaviour
{
    [SerializeField] TutorialController tutorialController;
    public Fish.Fish_Direction direction;

    private void Start()
    {
        StartCoroutine(SpawnFish());
    }

    IEnumerator SpawnFish()
    {
        while (true)
        {

            GameObject fishObj = Instantiate(AddressableManager.Instance.Get<GameObject>("Fish/ClownFish"),
                transform.position + Vector3.up * Random.Range(-3f, 3f), Quaternion.identity);

            Fish fish = fishObj.GetComponent<Fish>();
            fish.fish_Direction = direction;
            fish.fishedAcion = tutorialController.SuccessFish;
            if (fish.fish_Direction == Fish.Fish_Direction.Right)
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z));
            yield return new WaitForSeconds(11);
        }
    }
}
