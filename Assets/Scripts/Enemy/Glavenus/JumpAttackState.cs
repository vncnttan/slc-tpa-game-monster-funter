using UnityEngine;

public class JumpAttackState : EnemyBaseState{
    private float timer;
    public override void EnterState(EnemyStateManager em){
        timer = 3.9f;
        em.animator.SetTrigger("Jump");
        em.agent.enabled = false;
        LeanTween.moveX(em.enemyobject, em.player.transform.position.x, 3.8f);
        LeanTween.moveZ(em.enemyobject, em.player.transform.position.z, 3.8f);
        LeanTween.moveY(em.enemyobject, em.enemyobject.transform.position.y + 5f, 1.2f).setDelay(0.8f);
        LeanTween.moveY(em.enemyobject, em.player.transform.position.y, 1.2f).setDelay(1.8f);
    }
    public override void UpdateState(EnemyStateManager em){
        timer -= Time.deltaTime;
        if(timer < 0){
            em.agent.enabled = true;
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
        playerManager.player.HP -= 50f;
        playerManager.SwapState(playerManager.damagedState);
    }
}