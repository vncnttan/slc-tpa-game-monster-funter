using UnityEngine;
public class FireBreathState : EnemyBaseState{
    private float timer;
    private float continuousTimer;
    private ParticleSystem fire;
    public override void EnterState(EnemyStateManager em){
        em.animator.SetTrigger("Fire");
        fire = GameObject.Find("FlameStream").GetComponent<ParticleSystem>();
        fire.Play();
        timer = 6f;
        continuousTimer = 1f;
    }
    public override void UpdateState(EnemyStateManager em){
        timer -= Time.deltaTime;
        continuousTimer -= Time.deltaTime;
        if(timer < 0){
            em.SwapState(em.chaseState);
        }
        
        Vector3 lookRotate = new Vector3(em.player.transform.position.x - em.gameObject.transform.position.x, 0f, em.player.transform.position.z - em.gameObject.transform.position.z);
        Quaternion lookRotation = Quaternion.LookRotation(lookRotate);
        em.gameObject.transform.rotation = Quaternion.RotateTowards(em.gameObject.transform.rotation, lookRotation, 500f * Time.deltaTime);
        
    }
    public override void EndState(EnemyStateManager em){
        fire.Stop();
    }
    public override void CollidesWithPlayer(EnemyStateManager em)
    {
        PlayerStateManager playerManager = em.player.GetComponent<PlayerStateManager>();

        if(playerManager.currentState == playerManager.damagedState || playerManager.currentState == playerManager.paladinRoll){
            return;
        }

        if(continuousTimer < 0){
            playerManager.player.HP -= 5f;
            continuousTimer = 1f;
        }
    }
}