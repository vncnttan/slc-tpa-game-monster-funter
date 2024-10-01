using UnityEngine;

public class Paladin : PlayerParent
{
    public Paladin(){
        this.walkSpeed = 5f;
        this.runSpeed = 12f;
        this.HP = 100f;
        this.Stamina = 100f;
        this.cd1 = 5f;
        this.cd2 = 3f;
    }
}
