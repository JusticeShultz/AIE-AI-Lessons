using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : MonoBehaviour
{
    public float Speed = 15.0f;
    public float Interval = 2.0f;
    public float MaxSightDistance = 100;
    public LayerMask obstacleMask;
    public Animator anim;

    [HideInInspector] public GameObject inView;

    Rigidbody rgb;
    float NormalSpeed;

    void Start()
    {
        NormalSpeed = Speed;
        rgb = GetComponent<Rigidbody>();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Interval);

        if(inView == null)
            transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

        StartCoroutine(Spawn());
    }

    private void Update()
    {
        rgb.velocity = transform.forward * Speed;

        Ray ray = new Ray();
        RaycastHit hitInfo = new RaycastHit();
        ray.direction = transform.forward;
        ray.origin = transform.position;

        if (Physics.Raycast(ray, out hitInfo, obstacleMask))
        {
            if (hitInfo.distance < 4.0f && hitInfo.collider.gameObject.name != "HotDogVendor(Clone")
            {
                transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
            }
        }

        if (inView != null)
        {
            transform.LookAt(inView.transform.position);
            Speed = NormalSpeed * 3;
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
            Speed = NormalSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "HotDogVendor(Clone)")
            Destroy(collision.gameObject);
    }
}