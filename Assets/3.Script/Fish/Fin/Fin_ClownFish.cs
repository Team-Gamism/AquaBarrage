using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin_ClownFish : Fin
{
    protected override IEnumerator Effect()
    {
        transform.localScale =  transform.lossyScale * 8.5f;
        yield return new WaitForSeconds(0.0005f);
    }
}