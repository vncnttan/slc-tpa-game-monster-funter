using UnityEngine;

public class EnemyDeathState : EnemyBaseState{
    public override void EnterState(EnemyStateManager em){
        em.animator.SetTrigger("Death");
    }
    public override void UpdateState(EnemyStateManager em){

    }
    public override void EndState(EnemyStateManager em){

    }
    public override void CollidesWithPlayer(EnemyStateManager em)
    {
        
    }
}