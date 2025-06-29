using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carp_Fin : Fin
{
    protected override IEnumerator Effect()
    {
        yield return new WaitForSeconds(2.25f);
        GameObject player = GameManager.Instance.player.gameObject;
        float angle = Mathf.Atan2(player.transform.position.y -transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        q = Quaternion.Euler(0f, 0f, q.eulerAngles.z);

        for (int i = 0; i < 150; i++)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * 5);
            yield return new WaitForSeconds(0.0005f);
        }
    }
}
