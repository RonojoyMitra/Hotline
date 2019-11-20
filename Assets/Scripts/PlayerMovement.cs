using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //TODO add Animator variable
    [SerializeField] WeaponClass weapon;
    [SerializeField] BoxCollider interactCollider;
    [SerializeField] Transform weaponTransform;

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
        Movement();
        UseWeapon();
        ThrowPickupWeapon();  
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(x, 0, z);
        transform.Translate(new Vector3(dir.x * MoveSpeed * Time.deltaTime,0, dir.z * MoveSpeed * Time.deltaTime), Space.World);
    }

    void UseWeapon()
    {
        // left click to use weapon
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // TODO call animation
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
                // Change player sprite
                // Instantiate separate weapon Prefab
                weapon.Throw();
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
