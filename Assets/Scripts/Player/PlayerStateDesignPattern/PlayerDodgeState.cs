using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private float dodgeTime;
    private float dodgeSpeed;
    public override void EnterState(PlayerStateManager pm){
        pm.player._animator.SetBool("Dodge", true);
        dodgeTime = 1f;
        dodgeSpeed = 4f;
    }

    public override void UpdateState(PlayerStateManager pm){
        dodgeTime -= Time.deltaTime;
        if(dodgeTime < 0){
            pm.SwapState(pm.idleState);
        }

        Vector3 direction = Quaternion.Euler(0, 0, 2f) * pm.transform.forward * -1f + Quaternion.Euler(1f, 0, 0) * pm.transform.right;
        pm.controller.Move(direction * this.dodgeSpeed * Time.deltaTime);
    }

    public override void EndState(PlayerStateManager pm){
        pm.player._animator.SetBool("Dodge", false);
    }
}