using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    public GameObject Blood;

    private void Start()
    {
        StartCoroutine(EndMySuffering(0.5f));
    }

    private IEnumerator EndMySuffering(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Enemy")
        {
            if (collision.gameObject.GetComponent<Reference>().Object.GetComponent<EnemyAI>().current_target == null)
            {
                Instantiate(Blood, collision.contacts[0].point, transform.rotation);
                collision.gameObject.GetComponent<Reference>().Object.GetComponent<EnemyAI>().Die();
            }
        }
    }
}