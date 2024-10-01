using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gammoth : Enemy
{
    public Gammoth(){
        this.health = 30;
        this.maxHealth = 30;
        this.atkRange = 0.7f;
        this.sightRange = 20;
        this.delayDmg = 0.3f;
    }
}
