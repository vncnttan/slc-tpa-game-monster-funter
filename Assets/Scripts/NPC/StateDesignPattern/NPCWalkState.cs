using UnityEngine;
using UnityEngine.AI;

public class NPCWalkState : NPCBaseState
{
    private Vector3 randomDirection;
    private float walkRadius = 20f;
    Vector3 finalPosition;
    public override void EnterState(NPCStateManager npcm){
        this._animator.SetBool("Walk", true);
        randomDirection = Random.insideUnitSphere * walkRadius;

        randomDirection += this.npc.transform.position;

        NavMeshHit t;
        NavMesh.SamplePosition(randomDirection, out t, walkRadius, 1);
        this.agent.SetDestination(t.position);
        finalPosition = t.position;
    }

    public override void UpdateState(NPCStateManager npcm){
        if(NPCFunctions.NPCisNearPlayer(this.player, this.npc)){
            npcm.SwapState(npcm.lookState);
        } else if (finalPosition == this.npc.transform.position){
            npcm.SwapState(npcm.idleState);
        }
    }
    public override void EndState(NPCStateManager npcm){
        this._animator.SetBool("Walk", false);
    }
}
