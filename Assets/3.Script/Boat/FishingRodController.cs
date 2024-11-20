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
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float arrowSpeed = 45f;
    [SerializeField] private float launchForce = 100f;
    [SerializeField] private float rotation;
    [SerializeField] private float speed;
    [SerializeField] private float resetSpeed;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float reelSpeed = 10f;
    private InputAction castAction;
    private InputAction reelAction;
    private PlayerInput playerInput;
    private GameObject curPrefab;
    private bool isCasting = false;
    private bool isHoldingCast = false;
    private bool isReeling = false;
    private float curAngle = 45f;
    private int dir = 1;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        castAction = playerInput.actions["Cast"];
        reelAction = playerInput.actions["Reel"];

        castAction.started += context => StartCasting();
        castAction.canceled += context => StopCastingAndLaunch();
        reelAction.performed += context => StartReeling();

        Init();
    }

    private void Init()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.positionCount = 0;
    }

    private void OnEnable()
    {
        castAction.Enable();
        reelAction.Enable();
    }

    private void OnDisable()
    {
        castAction.Disable();
        reelAction.Disable();
    }

    private void StartCasting()
    {
        if (!isCasting && !isReeling && curPrefab == null)
        {
            isCasting = true;
            isHoldingCast = true;
            arrow.SetActive(true);
            StartCoroutine(MoveArrow());
        }
    }

    private void StopCastingAndLaunch()
    {
        if (isHoldingCast)
        {
            isHoldingCast = false;
            arrow.SetActive(false);
            StartCoroutine(CastFishingRod());
        }
    }

    private IEnumerator MoveArrow()
    {
        while (isHoldingCast)
        {
            curAngle += arrowSpeed * Time.deltaTime * dir;
            curAngle = Mathf.Clamp(curAngle, 25f, 45f);

            if (curAngle >= 45f && dir > 0)
            {
                dir = -1;
            }
            else if (curAngle <= 25f && dir < 0)
            {
                dir = 1;
            }
            arrow.transform.localEulerAngles = new Vector3(0, 0, curAngle);

            yield return null;
        }
    }

    private IEnumerator CastFishingRod()
    {
        yield return Rotate(rotation, speed);
        yield return new WaitForSeconds(waitTime);
        yield return Rotate(175f, resetSpeed);
        LaunchPrefab();
        StartCoroutine(DrawFishingLine());
        yield return new WaitForSeconds(0.1f);
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
        if (launchPrefab != null && launchPoint != null && curPrefab == null)
        {
            MeshRenderer meshRenderer = launchPoint.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            curPrefab = Instantiate(launchPrefab, launchPoint.position, Quaternion.identity);
            Rigidbody rb = curPrefab.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float angle = curAngle * Mathf.Deg2Rad - Mathf.PI;
                Vector3 force = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * launchForce;
                rb.AddForce(force, ForceMode.Impulse);
            }
        }
    }

    private IEnumerator ReelPrefab()
    {
        if (curPrefab != null)
        {
            Rigidbody rb = curPrefab.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
        isReeling = true;

        while (curPrefab != null && Vector3.Distance(curPrefab.transform.position, launchPoint.position) > 0.1f)
        {
            curPrefab.transform.position = Vector3.MoveTowards(curPrefab.transform.position, launchPoint.position, reelSpeed * Time.deltaTime);
            yield return null;
        }

        MeshRenderer meshRenderer = launchPoint.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
        }
        Destroy(curPrefab);
        ResetLine();
        isReeling = false;
    }

    private IEnumerator DrawFishingLine()
    {
        lineRenderer.positionCount = 20;

        while (curPrefab != null)
        {
            Vector3 startPoint = launchPoint.position;
            Vector3 endPoint = curPrefab.transform.position;

            float distance = Vector3.Distance(startPoint, endPoint);
            Vector3 controlPoint = Vector3.Lerp(startPoint, endPoint, 0.5f)
                + Vector3.up * Mathf.Clamp(distance * 0.1f, 0.1f, 1f);

            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                float t = i / (lineRenderer.positionCount - 1f);

                Vector3 dynamicControlPoint = Vector3.Lerp(startPoint, controlPoint, t * t);
                Vector3 pointOnCurve = CalculateBezierPoint(t, startPoint, dynamicControlPoint, endPoint);

                lineRenderer.SetPosition(i, pointOnCurve);
            }
            yield return null;
        }
        ResetLine();
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    private void ResetLine()
    {
        lineRenderer.positionCount = 0;
    }

    private void StartReeling()
    {
        if (curPrefab != null && !isReeling)
        {
            StartCoroutine(ReelPrefab());
        }
    }
}