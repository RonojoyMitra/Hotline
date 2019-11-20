using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] float bulletForce = 2500.0f;
    Vector3 parentForward;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(-transform.up * bulletForce);
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject testObject = collision.gameObject;
        HealthComponent healthComp = testObject.GetComponent<HealthComponent>();

        if (healthComp)
        {
            Debug.Log("healthComp found");
            healthComp.HandleHit(false);
        }
        else
        {
            Debug.Log("healtComp not found");
        }

        Destroy(this.gameObject);
    }
}
