using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementScript : MonoBehaviour
{
    private Rigidbody rb;
    public Transform[] PatrolPoints;
    private bool patrolling = true;
    private bool canSeePlayer = false;
    public int destPoint = 0;
    public float enemySpeed = 5;
    public float enemyChaseSpeed = 8;
    private bool reachedPoint = false;
    private GameObject Player;
    private Vector2 lastPlayerPosition;
    NavMeshAgent myAgent;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        LookForPlayer();
        if(patrolling && PatrolPoints.Length != 0)
        {
            PatrolPath();
        }
        if (canSeePlayer)
        {
            MoveTowardsPlayer();
        }

        
    }

    void PatrolPath()
    {
        //transform.LookAt(PatrolPoints[destPoint].position);
        //transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[destPoint].position, enemySpeed * Time.deltaTime);
        // Debug.Log(destPoint);

        //if (Vector3.Distance(transform.position, PatrolPoints[destPoint].position) == 0)
        //{
        //    Debug.Log("reached point");
        //    reachedPoint = true;
        //    destPoint = (destPoint + 1) % PatrolPoints.Length;
        //}
        //destPoint = destPoint + 1;
        

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
            Debug.Log("incrementpoint");
            destPoint = (destPoint + 1);
            reachedPoint = false;


        }

        if (destPoint >= PatrolPoints.Length)
        {
            destPoint = 0;
        }

        myAgent.destination = PatrolPoints[destPoint].position;
    }

    void MoveTowardsPlayer()
    {
        //Moves towards player
        patrolling = false;
        transform.LookAt(Player.transform.position);
        //transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, enemyChaseSpeed * Time.deltaTime);
    }

    void LookForPlayer()
    {
        //var hit = Physics.Raycast(transform.position, Vector2.left, 5f);
        //Debug.DrawRay(transform.position, Vector2.left, Color.cyan);
        //if (hit && hit.transform.name == "Player")
        //{
        //    lastPlayerPosition = Player.transform.position;
        //    canSeePlayer = true;
        //    Debug.Log(canSeePlayer);
        //}
        //if (hit && hit.transform.name != "Player")
        //{
        //    canSeePlayer = false;
        //    Debug.Log(canSeePlayer);
        //}


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
                Player = hit.collider.gameObject;
            }
        }
    }
}
