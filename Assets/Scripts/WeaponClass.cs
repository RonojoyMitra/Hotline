using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    [SerializeField] bool bIsBlunt;
    [SerializeField] float useResetTime;
    [SerializeField] float throwForce;
    [SerializeField] Sprite weaponSprite;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Use()
    {

    }

    public virtual void Throw()
    {
        if(gameObject.transform.parent != null)
        {
            Vector3 LaunchDir = gameObject.transform.parent.forward;
            LaunchDir.Normalize();

            gameObject.transform.parent = null;

            rb.isKinematic = false;
            rb.AddForce(LaunchDir * throwForce, ForceMode.Impulse);
        }
    }

    public void PickedUp()
    {
        rb.isKinematic = true;
    }
}
