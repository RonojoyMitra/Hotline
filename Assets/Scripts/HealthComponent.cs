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

                // TODO disable gameobject instead of destroying

                // Destroy(this.gameObject);
                if (this.gameObject.CompareTag("enemy"))
                {
                    GetComponent<EnemyMovementScript>().myAgent.isStopped = true;
                    GetComponent<Rigidbody>().isKinematic = true;
                    print("YOU ARE DONE, HARVEY");
                    
                    // TODO set object rotation to face source of hit

                    // debug messages to notify if animator status
                    if (animator)
                    {
                        Debug.Log("Animator found");
                    }
                    else
                    {
                        Debug.Log("Animator not found");
                    }

                    // play blood FX
                    animator.SetTrigger("Death");
                }
            }
        }          
    }
}