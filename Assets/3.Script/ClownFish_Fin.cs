using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownFish_Fin : Fin
{
    protected override IEnumerator Effect()
    {
        Debug.Log("이거 아니지?");
        for (int i = 11; i <= 30; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (0.1f * i), 0.25f);
                yield return new WaitForSeconds(0.0005f);
            }
        }
    }
}
