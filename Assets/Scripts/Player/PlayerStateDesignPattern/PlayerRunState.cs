using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private float footstepDelayTime = 0.3f;
    public override void EnterState(PlayerStateManager pm){
        pm.speed = pm.player.runSpeed;
        pm.player._animator.SetBool("Run", true);
    }

    public override void UpdateState(PlayerStateManager pm){
        if(footstepDelayTime < 0 && pm.controller.isGrounded){
            pm.footsteps[Random.Range(0, 9)].Play();
            footstepDelayTime = 0.3f;
        }
        footstepDelayTime -= Time.deltaTime;

        if(((!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) || pm.player.Stamina < 0)){
            pm.SwapState(pm.walkState);
        }

        pm.CheckState(pm.currentState);

        pm.player.Stamina -= 10f * Time.deltaTime;
    }

    public override void EndState(PlayerStateManager pm){
        pm.player._animator.SetBool("Run", false);
    }

    
}