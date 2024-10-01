using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glavenus : Enemy
{
    public Glavenus(){
        this.health = 600;
        this.maxHealth = 600;
        this.atkRange = 1f;
        this.sightRange = 30;
        this.delayDmg = 0.6f;
    }
}