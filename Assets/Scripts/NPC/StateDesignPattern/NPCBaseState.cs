using UnityEngine;
using UnityEngine.AI;
public abstract class NPCBaseState {
    internal GameObject npc;
    internal GameObject player;
    internal Animator _animator;
    internal NavMeshAgent agent;

    public abstract void EnterState(NPCStateManager npcm);
    public abstract void UpdateState(NPCStateManager npcm);
    public abstract void EndState(NPCStateManager npcm);
}