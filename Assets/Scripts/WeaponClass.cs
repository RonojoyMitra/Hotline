using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    [SerializeField] bool IsBlunt;
    [SerializeField] bool IsGun;
    [SerializeField] float useResetTime;
    [SerializeField] float throwForce;
    [SerializeField] Sprite weaponSprite;
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider boxCollider;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Use()
    {
        //TODO add animation call here

        if (!IsGun)
        {
            
            Collider[] targets = Physics.OverlapBox(boxCollider.transform.position, boxCollider.size / 2);

            foreach(Collider test in targets)
            {
                GameObject testObject = test.gameObject;
                HealthComponent healthComp = testObject.GetComponent<HealthComponent>();

                if (healthComp)
                {
                    Debug.Log("healthComp found");
                    healthComp.HandleHit(IsBlunt);
                }
                else
                {
                    Debug.Log("healtComp not found");
                }
            }
        }        
    }

    public virtual void Throw()
    {
        if(gameObject.transform.parent != null)
        {
            // get forward direction from parent object
            Vector3 LaunchDir = gameObject.transform.parent.forward;
            LaunchDir.Normalize();

            // detach self from parent object
            gameObject.transform.parent = null;

            // make this object use physics and throw
            rb.isKinematic = false;
            rb.AddForce(LaunchDir * throwForce, ForceMode.Impulse);
        }
    }

    public void PickedUp()
    {
        // called when picking up object to reset it to kinematic
        rb.isKinematic = true;
    }
}
