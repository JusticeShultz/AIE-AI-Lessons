using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    Vendor myDad;

    private void Start()
    {
        myDad = transform.parent.GetComponent<Vendor>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Student(Clone)" && myDad.inView == null)
        {
            myDad.inView = other.gameObject;
        }
    }
}
