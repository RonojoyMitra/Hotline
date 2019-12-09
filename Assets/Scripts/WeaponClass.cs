using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    [SerializeField] protected bool IsBlunt;   
    [SerializeField] protected float useResetTime;
    [SerializeField] protected float throwForce;   
    [SerializeField] protected string attackBoolName;
    [SerializeField] protected string animBoolName;
    [SerializeField] protected SpriteRenderer weaponSprite;
    [SerializeField] protected Collider pickUpCollider;
    [SerializeField] protected BoxCollider meleeCollider;

    public bool IsGun;    
    protected bool IsReseting = false;

    protected Animator animator;
    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Use()
    {
        if(!IsReseting)
        {
            //TODO add animation call here
            animator.SetBool(attackBoolName, true);

            // set reseting boolean so that player must wait before swinging or shooting again
            IsReseting = true;

            if (!IsGun)
            {
                Collider[] targets = Physics.OverlapBox(meleeCollider.transform.position, meleeCollider.size / 2);

                foreach (Collider test in targets)
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
                        Debug.Log("healthComp not found");
                    }
                }
            }

            Invoke("UseLock", useResetTime);
        }       
    }

    protected void UseLock()
    {
        IsReseting = false;
        animator.SetBool(attackBoolName, false);
    }

    public virtual void Throw()
    {
        //TODO
        if(gameObject.transform.parent != null)
        {
            // get forward direction from parent object
            Vector3 LaunchDir = gameObject.transform.parent.forward;
            LaunchDir.Normalize();

            animator.SetBool(animBoolName, false);
            animator = null;            

            // detach self from parent object
            gameObject.transform.parent = null;

            weaponSprite.enabled = true;
            pickUpCollider.enabled = true;

            // make this object use physics and throw
            if (rb)
            {
                rb.isKinematic = false;
                rb.AddForce(LaunchDir * throwForce, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("Rigidbody not found");
            }         
        }
    }

    public virtual void PickedUp()
    {
        // called when picking up object to reset it to kinematic
        if(rb)
        {
            rb.isKinematic = true;
            pickUpCollider.enabled = false;
            weaponSprite.enabled = false;

            if (transform.parent.parent.tag == "Player")
            {
                animator = GameObject.FindGameObjectWithTag("PlayerAnimator").GetComponent<Animator>();
                animator.SetBool(animBoolName, true);
            }
            else if(transform.parent.parent.tag == "enemy")
            {
                animator = gameObject.GetComponentInChildren<Animator>();
                animator.SetBool(animBoolName, true);

                if(animator)
                {
                    Debug.Log("Found Enemy Animator");
                }
                else
                {
                    Debug.Log("NO Enemy Animator");
                }             
            }
        }
        else
        {
            Debug.Log("Rigidbody not found");
        }
    }
}
