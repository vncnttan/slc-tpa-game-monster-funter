using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager pm){
        pm.player._animator.SetBool("Jump", false);
        pm.player._animator.SetBool("Walking", false);
        pm.player._animator.SetBool("Run", false);
    }

    public override void UpdateState(PlayerStateManager pm){
        if(pm.controller.isGrounded){
            pm.player._animator.SetBool("Grounded", true);
        }
        pm.CheckState(pm.currentState);
    }

    public override void EndState(PlayerStateManager pm){

    }

    
}