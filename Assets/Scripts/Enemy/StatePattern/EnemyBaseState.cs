public abstract class EnemyBaseState{
    public abstract void EnterState(EnemyStateManager npcm);
    public abstract void UpdateState(EnemyStateManager npcm);
    public abstract void EndState(EnemyStateManager npcm);
    public abstract void CollidesWithPlayer(EnemyStateManager npcm);
}