using UnityEngine;

public class NPCIdleState : NPCBaseState
{
    private float timer;
    public override void EnterState(NPCStateManager npcm){
        this.timer = Random.Range(20, 60);
    }
    public override void UpdateState(NPCStateManager npcm){
        timer -= Time.deltaTime;

        // Check for colliding NPC
        if(NPCFunctions.NPCisNearPlayer(this.player, this.npc)){
            npcm.SwapState(npcm.lookState);
        } else if (timer < 0){
            // Check if it should move
            npcm.SwapState(npcm.walkState);
        }
    }
    public override void EndState(NPCStateManager npcm){

    }
}
