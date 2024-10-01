using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager pm){
        pm.player._animator.SetBool("Jump", true);
        pm.player._animator.SetBool("Grounded", false);
        pm.v_velocity = 0.8f;
    }

    public override void UpdateState(PlayerStateManager pm){
        pm.player._animator.SetBool("Jump", false);
        if(pm.controller.isGrounded){
            pm.player._animator.SetBool("Grounded", true);
            pm.SwapState(pm.idleState);
        }
    }

    public override void EndState(PlayerStateManager pm){
        pm.player._animator.SetBool("Jump", false);
        pm.player._animator.SetBool("Grounded", true);
    }

    
}