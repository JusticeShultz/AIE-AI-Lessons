using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Camera;
    public float PlayerSpeed = 1.0f;
    Rigidbody MyRGB;

	// Use this for initialization
	void Start ()
    {
        MyRGB = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, transform.position + new Vector3(0, 15, 0), 0.1f);

        MyRGB.velocity = new 
        Vector3
        (
            Vector3.right.x * Input.GetAxis("Horizontal") * Time.deltaTime * PlayerSpeed, 
            0,
            Vector3.forward.z * Input.GetAxis("Vertical") * Time.deltaTime * PlayerSpeed
         );
    }
}
