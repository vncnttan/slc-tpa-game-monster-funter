using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeadState : PlayerBaseState{
    private float DeathTimer;
    public override void EnterState(PlayerStateManager pm){
        pm.player._animator.SetTrigger("Dead");
        DeathTimer = 3.6f;
    }

    public override void UpdateState(PlayerStateManager pm){
        DeathTimer -= Time.deltaTime;
        // Debug.Log(DeathTimer);
        if(DeathTimer < 0){
            this.EndState(pm);
        }
    }

    public override void EndState(PlayerStateManager pm){
        SceneManager.LoadScene("Lose");
    }
}