using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : WeaponClass
{
    [SerializeField] GameObject bullet;
    [SerializeField] int ammoCount;
    [SerializeField] float bulletForce;
    [SerializeField] Transform muzzle;

    public override void Use() 
    {
        // call base method for animation calls
        base.Use();

        // instantiate a bullet prefab adn set it's orientation to match the muzzle
        GameObject projectile = Instantiate(bullet);
        projectile.transform.position = muzzle.position;
        projectile.transform.rotation = muzzle.rotation;
        projectile.GetComponent<Rigidbody>().AddForce(bulletForce * muzzle.forward);
    }
}
