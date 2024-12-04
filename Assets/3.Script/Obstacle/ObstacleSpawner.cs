using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pirateShipPrefab;
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 0, -25);

    void Start()
    {
        StartCoroutine(SpawnPirateShip());
    }

    public IEnumerator SpawnPirateShip()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);

            int i = Random.Range(0, 3);
            Debug.Log("º÷" + i);

            if (!LevelManager.instance.isPrirate && i == 0 && !LevelManager.instance.isPausedGame)
            {
                LevelManager.instance.isPrirate = true;

                bool isDir = Random.value > 0.5f;
                Quaternion rotation = Quaternion.Euler(0, isDir ? 75 : -75, 0);

                GameObject pirateShip = Instantiate(pirateShipPrefab, spawnPosition, rotation);
                PirateShip shipScript = pirateShip.GetComponent<PirateShip>();

                if (shipScript != null)
                {
                    shipScript.Init(isDir);
                }

                Vector3 scale = pirateShip.transform.localScale;
                scale.x = isDir ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
                pirateShip.transform.localScale = scale;
            }
        }
    }
}