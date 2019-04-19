using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMe : MonoBehaviour
{
    public float Seconds = 2.5f;
    void Start()
    {
        StartCoroutine("DoADie");
    }

    IEnumerator DoADie()
    {
        yield return new WaitForSeconds(Seconds);
        Destroy(gameObject);
    }
}
