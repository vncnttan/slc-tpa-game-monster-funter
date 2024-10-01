using UnityEngine;

public class Inventory{
    internal int potion;
    internal int meat;

    // public abstract 

    public Inventory(int meat, int potion){
        this.meat = potion;
        this.potion = meat;
    }

    public void UsePotion(PlayerParent player){
        if(this.potion > 0){
            player._animator.SetTrigger("UseItem");
            player.HP += 50;
            this.potion -= 1;
        }
    }

    public void UseMeat(PlayerParent player){
        if(this.meat > 0){
            player._animator.SetTrigger("UseItem");
            player.Stamina = 100f;
            this.meat -= 1;
        }
    }
}