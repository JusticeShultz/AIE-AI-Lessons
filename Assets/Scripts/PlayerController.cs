using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Player;

    public GameObject _Camera;
    public float PlayerSpeed = 1.0f;
    public GameObject StabPoint;
    public float speed = 10;
    public GameObject ShankObject;
    public float MaxHealth = 100;
    public GameObject DeathPoint;
    public UnityEngine.UI.Image HurtEffect;

    [HideInInspector] public float CurrentHealth = 100;

    Rigidbody MyRGB;

	void Start ()
    {
        PlayerController.Player = this;
        StabPoint.transform.parent = null;
        MyRGB = GetComponent<Rigidbody>();	
	}

    void Update()
    {
        _Camera.transform.position = Vector3.Lerp(_Camera.transform.position, transform.position + new Vector3(0, 15, 0), 0.05f);

        MyRGB.velocity = new
        Vector3
        (
            Vector3.right.x * Input.GetAxis("Horizontal") * Time.deltaTime * PlayerSpeed,
            MyRGB.velocity.y,
            Vector3.forward.z * Input.GetAxis("Vertical") * Time.deltaTime * PlayerSpeed
         );

        if (CurrentHealth <= 0)
            transform.position = DeathPoint.transform.position;
    }

    void FixedUpdate()
    {
        Plane targetPlane = new Plane(Vector3.up, StabPoint.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;

        if (targetPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - StabPoint.transform.position);
            StabPoint.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed);
        }

        StabPoint.transform.position = transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject shank = Instantiate(ShankObject, StabPoint.transform.position + (StabPoint.transform.forward * 1), StabPoint.transform.rotation);
            shank.transform.parent = StabPoint.transform;
        }

        Debug.DrawLine(StabPoint.transform.position, StabPoint.transform.position + StabPoint.transform.forward * 4);
    }

    public void Damage(float damage, EnemyAI punchee)
    {
        if (Vector3.Distance(punchee.gameObject.transform.position, transform.position) < punchee.MaxHitDistance && punchee.current_target != null)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
            StartCoroutine("Hurt");
        }
    }

    IEnumerator Hurt()
    {
        HurtEffect.color = new Color(1, 0, 0, 0.35f);
        yield return new WaitForSeconds(0.25f);
        HurtEffect.color = Color.clear;
    }
}