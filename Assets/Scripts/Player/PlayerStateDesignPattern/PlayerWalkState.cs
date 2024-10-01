using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    float footstepDelayTime = 0.3f;
    public override void EnterState(PlayerStateManager pm){
        pm.player._animator.SetBool("Walking", true);
        pm.speed = pm.player.walkSpeed;
    }

    public override void UpdateState(PlayerStateManager pm){
        if(footstepDelayTime < 0 && pm.controller.isGrounded){
            pm.footsteps[Random.Range(0, 9)].Play();
            footstepDelayTime = 0.7f;
        }
        footstepDelayTime -= Time.deltaTime;
        pm.CheckState(pm.currentState);
    }

    public override void EndState(PlayerStateManager pm){
        pm.player._animator.SetBool("Walking", false);
    }

    
}