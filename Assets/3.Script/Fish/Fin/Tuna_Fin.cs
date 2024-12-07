using System.Collections;
using UnityEngine;

public class Tuna_Fin : Fin
{
    GameObject subBullet;
    private void Start()
    {
        subBullet = Resources.Load<GameObject>(($"Fish/Fish_Fin/Tuna_Fin_Sub"));
    }

    protected override IEnumerator Effect()
    {
        yield return new WaitForSeconds(1.5f);

        Instantiate(subBullet, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,  -40));
        Instantiate(subBullet, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
        Instantiate(subBullet, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 40));

        Destroy(gameObject);
    }
}
