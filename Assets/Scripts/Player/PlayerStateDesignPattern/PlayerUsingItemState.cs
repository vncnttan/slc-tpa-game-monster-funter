using UnityEngine;

public class PlayerUsingItemState : PlayerBaseState{
    private float UseItemTimer;
    public override void EnterState(PlayerStateManager pm){
        pm.player._animator.SetTrigger("UseItem");
        UseItemTimer = 0.6f;
    }

    public override void UpdateState(PlayerStateManager pm){
        UseItemTimer -= Time.deltaTime;

        if(UseItemTimer < 0){
            pm.SwapState(pm.idleState);
        }
    }

    public override void EndState(PlayerStateManager pm){
        
    }
}