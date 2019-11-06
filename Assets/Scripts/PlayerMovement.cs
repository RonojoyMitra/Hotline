using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] WeaponClass weapon;
    private Rigidbody rb;
    public float MoveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(weapon)
        {
            weapon.gameObject.transform.parent = this.gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(x, 0, z);

        Movement(dir);

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(weapon)
            {
                weapon.Throw();
            }
        }
    }

    void Movement(Vector3 dir)
    {
        transform.Translate(new Vector3(dir.x * MoveSpeed * Time.deltaTime,0, dir.z * MoveSpeed * Time.deltaTime), Space.World);
    }
}
