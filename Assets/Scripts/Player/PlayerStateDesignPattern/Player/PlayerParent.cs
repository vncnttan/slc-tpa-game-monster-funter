using UnityEngine;

public abstract class PlayerParent {

    // Skill 1 = R, skill 2 = F
    internal float cd1;
    internal float cd2;
    internal float Stamina;
    internal float HP;
    internal float walkSpeed;
    internal float runSpeed;
    internal float sk1gauge;
    internal float sk2gauge;
    internal Animator _animator;
    public void UpdateCharStats(PlayerStateManager pm){
        if(this.Stamina > 100){
            this.Stamina = 100;
        }
        if(this.HP > 100){
            this.HP = 100;
        }
        if(this.sk1gauge > 100){
            this.sk1gauge = 100;
        }
        if(this.sk2gauge > 100){
            this.sk2gauge = 100;
        }
        pm.healthSlider.value = this.HP;
        pm.staminaSlider.value = this.Stamina;
        pm.skill1Slider.value = this.sk1gauge;
        pm.skill2Slider.value = this.sk2gauge;
    }

    public virtual void StaminaRefill(float value){
        this.Stamina += value % 101;
    }

    public virtual void ReduceCooldownWithTime(){
        this.sk1gauge += Time.deltaTime * 100 / cd1;
        this.sk2gauge += Time.deltaTime * 100 / cd2;
    }
}