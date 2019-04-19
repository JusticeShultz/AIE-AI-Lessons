using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentSpawner : MonoBehaviour
{
    public GameObject ToSpawn;
    public float Interval = 3.5f;

    void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Interval);
        GameObject spawned = Instantiate(ToSpawn, transform.position, transform.rotation);
        spawned.GetComponent<Student>().Waypoint = GetComponent<Waypoint>().NextPoint;
        StartCoroutine("Spawn");
    }
}
