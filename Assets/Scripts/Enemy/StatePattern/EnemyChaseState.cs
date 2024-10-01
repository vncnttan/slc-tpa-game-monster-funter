using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState{
    public override void EnterState(EnemyStateManager em){
        em.agent.isStopped = false;
        em.animator.SetBool("Chasing", true);
        em.agent.SetDestination(em.player.transform.position);
    }
    public override void UpdateState(EnemyStateManager em){
        em.agent.SetDestination(em.player.transform.position);
        if(em.agent.remainingDistance >= em.agent.stoppingDistance){
            em.SwapState(em.availableAtkStates[Random.Range(0, em.availableAtkStates.Count)]);
        }
    }
    public override void EndState(EnemyStateManager em){
        em.animator.SetBool("Chasing", false);
    }
    public override void CollidesWithPlayer(EnemyStateManager em)
    {
        em.agent.isStopped = true;
    }
}