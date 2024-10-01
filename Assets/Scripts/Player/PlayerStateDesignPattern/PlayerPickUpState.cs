using UnityEngine;

public class PlayerPickUpState : PlayerBaseState
{
    private float pickupTimer;
    public override void EnterState(PlayerStateManager pm){
        pm.player._animator.SetTrigger("PickItem");
        pickupTimer = 2f;
    }

    public override void UpdateState(PlayerStateManager pm){
        pickupTimer -= Time.deltaTime;

        if(pickupTimer < 0){
            pm.SwapState(pm.idleState);
        }
    }

    public override void EndState(PlayerStateManager pm){
        
    }

    
}