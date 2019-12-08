using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] Animator animator;

    PointManager PM;

    public bool IsDead = false;

    private void Start()
    {
        PM = GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>();

        if(PM)
        {
            Debug.Log("Point manager found");
        }
        else
        {
            Debug.Log("Point manager was NOT found");
        }
    }

    public void HandleHit(bool IsBluntHit)
    {
        if (IsBluntHit)
        {
            //knockdown
        }
        else
        {
            if(!IsDead)
            {
                // disable collider
                GetComponent<Collider>().enabled = false;

                // count points
                if (!this.gameObject.CompareTag("Player"))
                {
                    PM.AddComboCount();
                    PM.AddPoints(this.gameObject.transform.position);

                    // disable the healthcomp when dead
                    IsDead = true;
                }
                else
                {
                    // if object is the player then handle death animation and set IsDead to true
                    if (animator)
                    {
                        Debug.Log("Animator found");

                        // play death animation
                        animator.SetTrigger("Death");
                    }
                    else
                    {
                        Debug.Log("Animator not found");
                    }

                    // disable the healthcomp when dead
                    IsDead = true;
                }

                // TODO disable gameobject instead of destroying
                if (this.gameObject.CompareTag("enemy"))
                {
                    GetComponent<EnemyMovementScript>().myAgent.isStopped = true;
                    //GetComponent<EnemyMovementScript>().DropWeapon();
                    GetComponent<Rigidbody>().isKinematic = true;
                    
                    // TODO set object rotation to face source of hit

                    // debug messages to notify if animator status
                    if (animator)
                    {
                        Debug.Log("Animator found");

                        // play death animation
                        animator.SetTrigger("Death");
                    }
                    else
                    {
                        Debug.Log("Animator not found");
                    }                   
                }
            }
        }          
    }
}