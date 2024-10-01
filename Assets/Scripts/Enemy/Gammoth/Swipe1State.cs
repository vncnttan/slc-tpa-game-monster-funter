using UnityEngine;
public class Swipe1State : EnemyBaseState{

    private float timer;
    public override void EnterState(EnemyStateManager em){
        em.animator.SetTrigger("Swipe1");
        timer = 3f;
    }
    public override void UpdateState(EnemyStateManager em){
        timer -= Time.deltaTime;
        if(timer < 0f){
            em.SwapState(em.idleState);
        }
    }
    public override void EndState(EnemyStateManager em){
        
    }
    public override void CollidesWithPlayer(EnemyStateManager em)
    {
        PlayerStateManager playerManager = em.player.GetComponent<PlayerStateManager>();

        if(playerManager.currentState == playerManager.damagedState || playerManager.currentState == playerManager.paladinRoll){
            return;
        }
        playerManager.player.HP -= 10f;
        playerManager.SwapState(playerManager.damagedState);
    }
}