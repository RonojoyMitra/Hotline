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
    [SerializeField] protected AudioSource audiosource;
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
  
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

                        audiosource.PlayOneShot(sound1);
                    }
                    else
                    {
                        Debug.Log("healthComp not found");
                        audiosource.PlayOneShot(sound2);
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
            audiosource.PlayOneShot(sound3);
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
                // get the player's Animator and set weapon hold animState
                animator = GameObject.FindGameObjectWithTag("PlayerAnimator").GetComponent<Animator>();
                if (animator)
                {
                    animator.SetBool(animBoolName, true);
                }
                else
                {
                    Debug.Log("NO Enemy Animator" + transform.parent.parent.name);
                }

                // TODO clean up getting meleeCollider from player
                meleeCollider = transform.parent.parent.GetChild(4).GetComponent<BoxCollider>();
                if(meleeCollider)
                {
                    Debug.Log("Found melee collider");
                }
                else
                {
                    Debug.Log("Melee collider not found, check GetChild index line 123");
                }
            }
            else if(transform.parent.parent.tag == "enemy")
            {
                // get the enemy Animator and set weapon hold animState
                animator = transform.parent.parent.GetComponentInChildren<Animator>();
                if (animator)
                {
                    animator.SetBool(animBoolName, true);
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
                    Debug.Log(gameObject.name + "Enemy melee collider not found, check GetChild index line 153");
                }
            }
        }
        else
        {
            Debug.Log("Rigidbody not found");
        }
    }
}
