using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : WeaponClass
{
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletForce;
    [SerializeField] Transform muzzle;

    public override void Use() 
    {
        GameObject projectile = Instantiate(bullet);
        projectile.transform.position = muzzle.position;
        projectile.GetComponent<Rigidbody>().AddForce(bulletForce * muzzle.forward);
    }
}
