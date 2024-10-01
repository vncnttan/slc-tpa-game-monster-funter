using UnityEngine;
using UnityEngine.AI;
public abstract class PlayerBaseState {
    public abstract void EnterState(PlayerStateManager pm);
    public abstract void UpdateState(PlayerStateManager pm);
    public abstract void EndState(PlayerStateManager pm);
}