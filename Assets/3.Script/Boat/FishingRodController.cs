using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingRodController : MonoBehaviour
{
    [SerializeField] private Transform rightShoulderTransform;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject launchPrefab;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float arrowSpeed = 45f;
    [SerializeField] private float launchForce = 100f;
    [SerializeField] private float rotation;
    [SerializeField] private float speed;
    [SerializeField] private float resetSpeed;
    [SerializeField] private float waitTime = 1f;
    private InputAction castAction;
    private PlayerInput playerInput;
    private bool isCasting = false;
    private bool isHoldingCast = false;
    private float curAngle = 0f;
    private int dir = 1;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        castAction = playerInput.actions["Cast"];

        castAction.started += context => StartCasting();
        castAction.canceled += context => StopCastingAndLaunch();
    }

    private void OnEnable()
    {
        castAction.Enable();
    }

    private void OnDisable()
    {
        castAction.Disable();
    }

    private void StartCasting()
    {
        if (!isCasting)
        {
            isCasting = true;
            isHoldingCast = true;
            arrow.SetActive(true);
            StartCoroutine(MoveArrow());
        }
    }

    private void StopCastingAndLaunch()
    {
        isHoldingCast = false;
        arrow.SetActive(false);
        StartCoroutine(CastFishingRod());
    }

    private IEnumerator MoveArrow()
    {
        while (isHoldingCast)
        {
            curAngle += arrowSpeed * Time.deltaTime * dir;

            if (curAngle >= 45f || curAngle <= -45f)
            {
                dir *= -1;
            }
            arrow.transform.localEulerAngles = new Vector3(0, 0, curAngle);
            yield return null;
        }
    }

    private IEnumerator CastFishingRod()
    {
        yield return Rotate(rotation, speed);
        yield return new WaitForSeconds(waitTime);
        yield return Rotate(-180f, resetSpeed);
        yield return new WaitForSeconds(0.1f);
        LaunchPrefab();
        isCasting = false;
    }

    private IEnumerator Rotate(float targetZ, float speed)
    {
        while (Mathf.Abs(GetZ() - targetZ) > 0.01f)
        {
            float z = Mathf.MoveTowardsAngle(GetZ(), targetZ, speed * Time.deltaTime);
            SetZ(z);
            yield return null;
        }
        SetZ(targetZ);
    }

    private float GetZ()
    {
        float z = rightShoulderTransform.localEulerAngles.z;
        return z >= 180f ? z - 360f : z;
    }

    private void SetZ(float z)
    {
        Vector3 euler = rightShoulderTransform.localEulerAngles;
        rightShoulderTransform.localRotation = Quaternion.Euler(euler.x, euler.y, z);
    }

    private void LaunchPrefab()
    {
        if (launchPrefab != null && launchPoint != null)
        {
            GameObject prefab = Instantiate(launchPrefab, launchPoint.position, Quaternion.identity);
            Rigidbody rb = prefab.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float angle = curAngle * Mathf.Deg2Rad;
                Vector3 force = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * launchForce;
                rb.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}