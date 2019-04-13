using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public GameObject Body;
    public Animator animator;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    bool PlayerIsSeeable = false;

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
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
                    transform.LookAt(targetPostition);
                }
            }
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
}
