using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    public GameObject NextPoint;
	
	void Update ()
    {
        if (NextPoint != null)
            Debug.DrawLine(transform.position, NextPoint.transform.position);
	}
}
