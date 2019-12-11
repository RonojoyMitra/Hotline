using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : WeaponClass
{
    [SerializeField] GameObject bullet;
    [SerializeField] int magSize = 25;
    [SerializeField] int ammoCount;
    [SerializeField] Transform muzzle;
    public AudioSource audiosource;
    public AudioClip gunsound2;
    

    protected override void Start()
    {
        base.Start();
        IsGun = true;
        ammoCount = magSize;
    }

    public override void Use() 
    {
        if (!IsReseting)
        {
            if (ammoCount > 0)
            {
                // call base method for animation calls
                base.Use();

                // instantiate a bullet prefab and set it's orientation to match the muzzle
                for(int i = 0; i < 6; i++)
                {
                    // Debug.Break();

                    GameObject projectile = Instantiate(bullet);
                    projectile.transform.position = muzzle.position;
                    
                    Quaternion projectileRot = muzzle.rotation;
                    projectile.transform.rotation = Quaternion.Euler(projectileRot.eulerAngles.x + -90, projectileRot.eulerAngles.y + Random.Range(-20.0f, 20.0f),
                        projectileRot.eulerAngles.z);

                    Debug.Log("Shotgun Bullet");
                }

                ammoCount--;
                audiosource.PlayOneShot(gunsound2);
            }

            Invoke("UseLock", useResetTime);
        }
    }

    public override void PickedUp()
    {
        base.PickedUp();       
    }
}
