using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
Projectile Spec
Specification of a projectile
*/

public class ProjectileSpec
{
    public int damage;

    public ProjectileSpec(int damage)
    {
        this.damage = damage;
    }
}