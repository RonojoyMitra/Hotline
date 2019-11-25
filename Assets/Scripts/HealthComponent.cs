using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    PointManager PM;

    private void Start()
    {
        PM = GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>();
    }

    public void HandleHit(bool IsBluntHit)
    {
        if (IsBluntHit)
        {
            //knockdown
        }
        else
        {
            if(!this.gameObject.CompareTag("Player"))
            {
                PM.AddComboCount();
                PM.AddPoints(this.gameObject.transform.position);
            }           
            
            //play blood FX
            //TODO disable gameobject instead of destroying
            Destroy(this.gameObject);
        }
    }
}