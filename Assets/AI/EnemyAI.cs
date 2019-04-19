using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float move_speed = 1.25f;
    public float viewRadius;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public GameObject Body;
    public GameObject RootRotation;
    public Animator animator;
    public float MaxHitDistance = 3.0f;
    public float AttackRate = 1.5f;

    [Range(0, 360)] public float viewAngle;

    [HideInInspector] public Transform current_target;
    [HideInInspector] public bool CanAttack = true;
    [HideInInspector] public List<Transform> visibleTargets = new List<Transform>();

    private Rigidbody body_RigidBody;

    bool PlayerIsSeeable = false;
    

    void Start()
    {
        RootRotation.transform.parent = null;
        body_RigidBody = Body.GetComponent<Rigidbody>();

        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    private void Update()
    {
        RootRotation.transform.position = Body.transform.position;
        Body.transform.rotation = Quaternion.Slerp(Body.transform.rotation, new Quaternion(0, RootRotation.transform.rotation.y, 0, RootRotation.transform.rotation.w), 0.25f);

        if (current_target != null)
        {
            if (Vector3.Distance(transform.position, current_target.transform.position) <= MaxHitDistance && CanAttack)
            {
                CanAttack = false;
                StartCoroutine("QueueAttack");
            }
            else if (Vector3.Distance(transform.position, current_target.transform.position) > MaxHitDistance && CanAttack)
                if (animator.GetBool("PlayerSeen") && animator.GetBool("PlayerNearby") && animator.GetBool("CanMove") && animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
                    body_RigidBody.velocity = Body.transform.forward * move_speed;
        }

        animator.SetBool("CanMove", CanAttack);
    }

    IEnumerator QueueAttack()
    {
        animator.SetInteger("AttackType", Random.Range(1, 4));
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(AttackRate);
        CanAttack = true;
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        PlayerIsSeeable = targetsInViewRadius.Length > 0;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);

                    Vector3 targetPostition = new Vector3(target.position.x, transform.position.y, target.position.z);
                    RootRotation.transform.LookAt(targetPostition);
                    current_target = target;
                }
            }
            else current_target = null;
        }

        if(visibleTargets.Count > 0)
        {
            animator.SetBool("PlayerSeen", true);
        }
        else
        {
            animator.SetBool("PlayerSeen", false);
        }

        animator.SetBool("PlayerNearby", PlayerIsSeeable);
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void Die()
    {
        print("Guess I'll die");
        animator.SetInteger("DeathType", Random.Range(1, 4));
        animator.SetInteger("Health", 0);
        Body.GetComponent<Rigidbody>().isKinematic = true;
        Body.GetComponent<BoxCollider>().enabled = false;
        enabled = false;
    }
}