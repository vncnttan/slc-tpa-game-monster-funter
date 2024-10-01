using UnityEngine;

public class Wizard : PlayerParent
{
    public Wizard(){
        this.walkSpeed = 5f;
        this.runSpeed = 12f;
        this.HP = 100f;
        this.Stamina = 100f;
        this.cd1 = 10f;
        this.cd2 = 5f;
    }
}
