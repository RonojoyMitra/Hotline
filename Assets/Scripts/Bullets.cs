using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
