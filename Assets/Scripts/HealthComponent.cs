using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public void HandleHit(bool IsBluntHit)
    {
        if (IsBluntHit)
        {
            //knockdown
        }
        else
        {
            //play blood FX
            Destroy(this.gameObject);
        }
    }
}
