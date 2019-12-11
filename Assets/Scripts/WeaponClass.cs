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
    [SerializeField] Rigidbody rb;
  
    public bool IsGun;    
    protected bool IsReseting = false;

    protected Animator animator;
    protected BoxCollider meleeCollider;

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

                meleeCollider = transform.parent.parent.GetChild(4).GetComponent<BoxCollider>();

                if(meleeCollider)
                {
                    Debug.Log("Found melee collider");
                }
                else
                {
                    Debug.Log("Melee collider not found, check GetChild index line 114");
                }
            }
            else if(transform.parent.parent.tag == "enemy")
            {
                animator = transform.parent.parent.GetComponentInChildren<Animator>();

                if (animator)
                {
                    animator.SetBool(animBoolName, true);
                    // Debug.Log("Found Enemy Animator");
                }
                else
                {
                    Debug.Log("NO Enemy Animator" + transform.parent.parent.name);
                }

                meleeCollider = transform.parent.parent.GetChild(2).GetComponent<BoxCollider>();

                if (meleeCollider)
                {
                    Debug.Log(gameObject.name + "Found melee collider");
                }
                else
                {
                    Debug.Log(gameObject.name + "Melee collider not found, check GetChild index line 114");
                }
            }
        }
        else
        {
            Debug.Log("Rigidbody not found");
        }
    }
}
