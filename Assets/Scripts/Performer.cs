using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Performer : MonoBehaviour
{
    public float Speed = 15.0f;
    public float Interval = 2.0f;
    public LayerMask obstacleMask;
    public Animator anim;
    public Animator anim2;

    Rigidbody rgb;
    bool canMove = true;

    void Start()
    {
        rgb = GetComponent<Rigidbody>();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Interval);

        transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

        canMove = true;
        anim2.SetBool("Moving", true);

        yield return new WaitForSeconds(Interval * 3);

        canMove = false;
        anim2.SetBool("Moving", false);

        yield return new WaitForSeconds(Interval);

        rgb.velocity = Vector3.zero;

        anim.SetTrigger("Perform");
        anim2.SetTrigger("Perform");

        yield return new WaitForSeconds(Interval * 2);

        canMove = true;
        anim2.SetBool("Moving", true);

        StartCoroutine(Spawn());
    }

    private void Update()
    {
        if(canMove)
            rgb.velocity = transform.forward * Speed;

        Ray ray = new Ray();
        RaycastHit hitInfo = new RaycastHit();
        ray.direction = transform.forward;
        ray.origin = transform.position;

        if (Physics.Raycast(ray, out hitInfo, obstacleMask))
        {
            if (hitInfo.distance < 4.0f)
            {
                transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
            }
        }
    }
}