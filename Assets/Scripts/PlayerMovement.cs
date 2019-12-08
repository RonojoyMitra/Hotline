using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] WeaponClass weapon;
    [SerializeField] BoxCollider interactCollider;
    [SerializeField] Transform weaponTransform;
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator feetAnimator;

    private Rigidbody rb;
    public float MoveSpeed = 5;

    bool SetWalk = false;

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
        Movement();
        UseWeapon();
        ThrowPickupWeapon();  
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // handle the animation for walking
        if(x != 0 || z != 0)
        {
            if(!SetWalk)
            {
                bodyAnimator.SetBool("IsWalking", true);
                feetAnimator.SetBool("IsWalking", true);
                Debug.Log("Set Walking to True");
                SetWalk = true;
            }
            
        }
        else if(x == 0 && z == 0)
        {
            bodyAnimator.SetBool("IsWalking", false);
            feetAnimator.SetBool("IsWalking", false);
            Debug.Log("Set Walking to false");
            SetWalk = false;
        }

        Vector3 dir = new Vector3(x, 0, z);
        transform.Translate(new Vector3(dir.x * MoveSpeed * Time.deltaTime,0, dir.z * MoveSpeed * Time.deltaTime), Space.World);
    }

    void UseWeapon()
    {
        // left click to use weapon
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // check if we have a weapon assigned to var
            if (weapon)
            {
                weapon.Use();
            }
        }
    }

    void ThrowPickupWeapon()
    {
        //On right click throw or pickup weapon
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // check if we have a weapon assigned to var
            if (weapon)
            {
                weapon.Throw();

                // unassign weapon variable
                weapon = null;
                Invoke("PickUp", 0.1f);
            }
            else
            {
                PickUp();
            }
        }
    }

    void PickUp()
    {
        Collider[] Items = Physics.OverlapBox(interactCollider.transform.position, interactCollider.size / 2);
        foreach (Collider testCol in Items)
        {
            if(weapon == null)
            {
                GameObject pickupItem = testCol.gameObject;
                WeaponClass newWeapon = pickupItem.GetComponent<WeaponClass>();
                if (newWeapon)
                {
                    // assign the new weapon to weapon variable and do a bunch of stuff to make position right
                    weapon = newWeapon;
                    weapon.gameObject.transform.position = weaponTransform.position;
                    weapon.gameObject.transform.rotation = weaponTransform.rotation;
                    weapon.gameObject.transform.SetParent(weaponTransform);
                    weapon.PickedUp();
                }
            }           
        }
    }
}
