using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEngine 
{
    public float calculateDamage(float projectileDamage, float multiplier)
    {
        return projectileDamage * multiplier;
    }
}