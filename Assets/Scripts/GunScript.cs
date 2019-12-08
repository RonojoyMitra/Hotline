using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : WeaponClass
{
    [SerializeField] GameObject bullet;
    [SerializeField] int magSize = 25;
    [SerializeField] int ammoCount;
    [SerializeField] Transform muzzle;

    protected override void Start()
    {
        base.Start();
        IsGun = true;
        ammoCount = magSize;
    }

    public override void Use() 
    {
        

        if(ammoCount > 0)
        {
            // call base method for animation calls
            base.Use();

            // instantiate a bullet prefab and set it's orientation to match the muzzle
            GameObject projectile = Instantiate(bullet);
            projectile.transform.position = muzzle.position;
            
            Quaternion projectileRot = muzzle.rotation;
            projectile.transform.rotation = Quaternion.Euler(projectileRot.eulerAngles.x + -90, projectileRot.eulerAngles.y,
                projectileRot.eulerAngles.z);

            ammoCount--;
        }      
    }

    public override void PickedUp()
    {
        base.PickedUp();       
    }
}
