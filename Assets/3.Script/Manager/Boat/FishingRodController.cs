using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingRodController : MonoBehaviour
{
    [SerializeField] private Transform rightShoulderTransform;
    [SerializeField] private float rotation;
    [SerializeField] private float speed;
    [SerializeField] private float resetSpeed;
    [SerializeField] private float waitTime = 1f;
    private bool isCasting = false;

    public void OnCast()
    {
        if (!isCasting)
        {
            StartCoroutine(CastFishingRod());
        }
    }

    private IEnumerator CastFishingRod()
    {
        isCasting = true;

        yield return Rotate(rotation, speed);
        yield return new WaitForSeconds(waitTime);
        yield return Rotate(-180f, resetSpeed);

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
}