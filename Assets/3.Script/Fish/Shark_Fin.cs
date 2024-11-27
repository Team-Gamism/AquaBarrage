using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark_Fin : Fin
{
    Vector3 startPos;

    protected override IEnumerator Effect()
    {
        speed = 0;
        yield return null;
        startPos = new Vector3(Random.Range(-25,25), Random.Range(-19, -10),0);
        transform.DOMove(startPos, 2).onComplete += Shoot;
        float angle = Mathf.Atan2(startPos.y - transform.position.y,startPos.y -transform.position.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        q = Quaternion.Euler(0f, 0f, q.eulerAngles.z);
        transform.DORotateQuaternion(q, 0.5f);
    }

    void Shoot()
    {
        GameObject player = GameObject.Find("Boat");
        float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        q = Quaternion.Euler(0f, 0f, q.eulerAngles.z);
        transform.DORotateQuaternion(q, 0.5f).onComplete += () => { speed = 10; };
    }
}
