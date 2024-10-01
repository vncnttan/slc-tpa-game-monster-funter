using UnityEngine;
public class SwipeAttackState : EnemyBaseState{
    private float timer;
    public override void EnterState(EnemyStateManager em){
        timer = 5f;
        em.animator.SetTrigger("Swipe");
    }
    public override void UpdateState(EnemyStateManager em){
        timer -= Time.deltaTime;
        if(timer < 0){
            em.SwapState(em.chaseState);
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
        playerManager.player.HP -= 20f;
        playerManager.SwapState(playerManager.damagedState);
    }
}