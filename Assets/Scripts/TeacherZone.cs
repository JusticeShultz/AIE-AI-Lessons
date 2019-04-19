using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherZone : MonoBehaviour
{
    Teacher myDad;

    private void Start()
    {
        myDad = transform.parent.GetComponent<Teacher>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "HotDogVendor(Clone)" && myDad.inView == null)
        {
            myDad.inView = other.gameObject;
        }
    }
}
