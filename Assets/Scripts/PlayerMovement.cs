using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(x, 0, z);

        Movement(dir);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(weapon)
            {
                weapon.Use();
            }
        }

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(weapon)
            {
                weapon.Throw();
                weapon = null;
            }
            else
            {
                Collider[] Items = Physics.OverlapBox(interactCollider.transform.position, interactCollider.size / 2);
                foreach(Collider testCol in Items)
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

    void Movement(Vector3 dir)
    {
        transform.Translate(new Vector3(dir.x * MoveSpeed * Time.deltaTime,0, dir.z * MoveSpeed * Time.deltaTime), Space.World);
    }
}
