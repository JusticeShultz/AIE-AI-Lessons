using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    public float Speed = 5.0f;
    public float WaypointDistanceChange = 2.0f;

    [HideInInspector] public GameObject Waypoint;
    Rigidbody rgb;

    private void Start()
    {
        rgb = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        if (Vector3.Distance(transform.position, Waypoint.transform.position) < WaypointDistanceChange)
            Waypoint = Waypoint.GetComponent<Waypoint>().NextPoint;

        transform.LookAt(Waypoint.transform.position);
        rgb.velocity = transform.forward * Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "End")
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Radial_Death") Destroy(gameObject);
    }
}
