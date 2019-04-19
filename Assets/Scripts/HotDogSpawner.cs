using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDogSpawner : MonoBehaviour
{
    public GameObject ToSpawn;
    public float Interval = 3.5f;

    GameObject MySon;

    void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Interval);

        if (MySon == null)
            MySon = Instantiate(ToSpawn, transform.position, transform.rotation);

        StartCoroutine("Spawn");
    }
}
