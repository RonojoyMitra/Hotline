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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        LookForPlayer();
        if(patrolling)
        {
            PatrolPath();
        }
        if (canSeePlayer)
        {
            MoveTowardsPlayer();
        }

        if (reachedPoint)
        {
            destPoint = (destPoint + 1) % PatrolPoints.Length;
            reachedPoint = false;
        }
    }

    void PatrolPath()
    {
        transform.LookAt(PatrolPoints[destPoint].position);
        transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[destPoint].position, enemySpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, PatrolPoints[destPoint].position) == 0)
        {
            Debug.Log("reached point");
            reachedPoint = true;
        }
    }

    void MoveTowardsPlayer()
    {
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
