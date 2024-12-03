using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShip : MonoBehaviour
{
    [SerializeField] private Transform[] cannonPoints;
    [SerializeField] private GameObject[] cannonAlert;
    [SerializeField] private float warningDuration = 1f;
    [SerializeField] private float fireDelay = 2f;
    [SerializeField] private float cannonSpeed = 10f;
    [SerializeField] private float rotationSpeed = 2f;
    private Vector3[] fireRotation;
    private bool isDir;

    [SerializeField] AudioClip cannonClip;

    public void Init(bool isDir)
    {
        this.isDir = isDir;

        fireRotation = isDir ? new Vector3[] { new Vector3(0, 30, 0), Vector3.zero, new Vector3(0, -30, 0), new Vector3(0, -75, 0) }
                             : new Vector3[] { new Vector3(0, -30, 0), Vector3.zero, new Vector3(0, 30, 0), new Vector3(0, 75, 0) };
        StartCoroutine(HandleCannonFireRoutine());
    }

    private void Start()
    {
        foreach (var alert in cannonAlert)
        {
            alert.SetActive(false);
        }
    }

    private IEnumerator HandleCannonFireRoutine()
    {
        int index = 0;
        while (true)
        {
            yield return StartCoroutine(RotateFirePosition(index));
            if (index == fireRotation.Length - 1)
            {
                LevelManager.instance.isPrirate = false;

                Destroy(gameObject);
                break;
            }
            yield return StartCoroutine(FireCannonsRoutine(index));
            index = (index + 1) % fireRotation.Length;
            yield return new WaitForSeconds(fireDelay);
        }
    }

    private IEnumerator FireCannonsRoutine(int index)
    {
        bool[] firePattern = FirePattern();

        for (int i = 0; i < cannonAlert.Length; i++)
        {
            cannonAlert[i].SetActive(firePattern[i]);
        }
        yield return new WaitForSeconds(warningDuration);

        for (int i = 0; i < cannonPoints.Length; i++)
        {
            if (firePattern[i])
            {
                FireCannon(i);
            }
        }
        for (int i = 0; i < cannonAlert.Length; i++)
        {
            cannonAlert[i].SetActive(false);
        }
    }

    private bool[] FirePattern()
    {
        bool[] pattern = new bool[5];
        int start = Random.Range(0, 4);
        int end = start + 1;

        for (int i = 0; i < pattern.Length; i++)
        {
            pattern[i] = true;
        }
        for (int i = start; i <= end; i++)
        {
            pattern[i] = false;
        }
        return pattern;
    }

    private void FireCannon(int index)
    {
        GameObject cannonBallPrefab = Resources.Load<GameObject>("Obstacle/PirateShip/CannonBall");
        GameObject cannonBall = Instantiate(cannonBallPrefab, cannonPoints[index].position, cannonPoints[index].rotation);

        GameManager.Instance.effectAudioSource.PlayOneShot(cannonClip);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 velocity = isDir ? -cannonPoints[index].right * cannonSpeed
                                     : cannonPoints[index].right * cannonSpeed;
            rb.AddForce(velocity, ForceMode.Impulse);
        }

        GameObject smokeEffectPrefab = Resources.Load<GameObject>("Obstacle/PirateShip/Effect/SmokeEffect");
        if (smokeEffectPrefab != null)
        {
            GameObject smokeEffect = Instantiate(smokeEffectPrefab, cannonPoints[index].position, cannonPoints[index].rotation);
            smokeEffect.transform.SetParent(cannonPoints[index]);
            Destroy(smokeEffect, smokeEffect.GetComponent<ParticleSystem>().main.duration);
        }
    }

    private IEnumerator RotateFirePosition(int index)
    {
        Vector3 firePos = fireRotation[index];
        Quaternion target = Quaternion.Euler(firePos);

        while (Quaternion.Angle(transform.rotation, target) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = target;
    }
}