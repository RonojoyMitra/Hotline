using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementScript : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    WeaponClass enemyWeapon;

    [SerializeField] Transform weaponTransform;
    [SerializeField] float meleeDistance;

    Rigidbody rb; 
    GameObject Player;
    Vector2 lastPlayerPosition;
    public Transform[] PatrolPoints;
    public NavMeshAgent myAgent;

    public bool heardPlayer = false;
    bool reachedPoint = false;
    bool IsPatrolling = true;
    bool canSeePlayer = false;
    public int destPoint = 0;
    public float enemySpeed = 5;
    public float enemyChaseSpeed = 8;

    HealthComponent healthComp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAgent = GetComponent<NavMeshAgent>();
        healthComp = GetComponent<HealthComponent>();
        Player = GameObject.FindGameObjectWithTag("Player");

        if(healthComp)
        {
            Debug.Log("HealthComp found on: " + this.gameObject.name);
        }
        else
        {
            Debug.Log("HealthComp NOT found on: " + this.gameObject.name);
        }

        GameObject tempWeapon = Instantiate(weaponPrefab);

        enemyWeapon = tempWeapon.GetComponent<WeaponClass>();

        if(enemyWeapon)
        {
            enemyWeapon.gameObject.transform.position = weaponTransform.position;
            enemyWeapon.gameObject.transform.rotation = weaponTransform.rotation;
            enemyWeapon.gameObject.transform.SetParent(weaponTransform);
            enemyWeapon.PickedUp();
            Debug.Log("Called PickedUp");
        }
        else
        {
            Debug.Log(this.gameObject.name + " does NOT have a weapon");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!healthComp.IsDead && !Player.GetComponent<HealthComponent>().IsDead)
        {
            LookForPlayer();

            if (IsPatrolling && PatrolPoints.Length != 0)
            {
                PatrolPath();
            }

            // move towards player and attack
            if (canSeePlayer || heardPlayer)
            {
                MoveTowardsPlayer();

                if(enemyWeapon)
                {
                    // Check again if enemy has line of sight on player
                    if(canSeePlayer)
                    {
                        if (enemyWeapon.IsGun)
                        {
                            enemyWeapon.Use();
                        }
                        else if (Vector3.Distance(transform.position, Player.transform.position) <= meleeDistance)
                        {
                            // if using a melee weapon wait until in proper distance to use
                            enemyWeapon.Use();
                        }
                    }                 
                }
            }
        }         
    }

    void PatrolPath()
    {
        //patrols the points set in PatrolPoints
        if (myAgent.remainingDistance <= .2f)
        {
            //destPoint = (destPoint + 1) % PatrolPoints.Length;
            Debug.Log("reached point");
            reachedPoint = true;
        }


        if (reachedPoint)
        {
            //destPoint = (destPoint + 1) % PatrolPoints.Length;
            //destPoint = destPoint + 1;
            Debug.Log("increment point");
            destPoint = (destPoint + 1);
            reachedPoint = false;
        }

        if (destPoint >= PatrolPoints.Length)
        {
            destPoint = 0;
        }

        myAgent.destination = PatrolPoints[destPoint].position;
    }

    public void MoveTowardsPlayer()
    {
        //Moves towards player
        IsPatrolling = false;
        transform.LookAt(Player.transform.position);
        //transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, enemyChaseSpeed * Time.deltaTime);
    }

    void LookForPlayer()
    {
        //casts a ray that looks for the player
        Ray lookRay = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        float maxLookRayDistance = 10f;

        Debug.DrawRay(lookRay.origin, lookRay.direction * maxLookRayDistance, Color.blue);

        if (Physics.Raycast(lookRay, out hit, maxLookRayDistance))
        {
            if (hit.transform.tag == "Player")
            {
                canSeePlayer = true;
                //Debug.Log("canseeplayer");
                Player = hit.collider.gameObject;
            }
            else
            {
                canSeePlayer = false;
            }
        }
    }

    public void DropWeapon()
    {
        enemyWeapon.Throw();
    }
}
