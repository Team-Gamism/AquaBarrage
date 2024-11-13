using System.Collections;
using UnityEngine;

public class Tuna_Fin : Fin
{

    protected override IEnumerator Effect()
    {
        yield return new WaitForSeconds(1.5f);

        Instantiate(Resources.Load(($"Fish/Fish_Fin/Tuna_Fin_Sub")), transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,  -40));
        Instantiate(Resources.Load(($"Fish/Fish_Fin/Tuna_Fin_Sub")), transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
        Instantiate(Resources.Load(($"Fish/Fish_Fin/Tuna_Fin_Sub")), transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 40));

        Destroy(gameObject);
    }
}
