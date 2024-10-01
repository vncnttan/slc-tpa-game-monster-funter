using UnityEngine;

public class PalBasicAtkState : PlayerBaseState
{
    private int atkCounter = 0;
    private float comboChainTime;
    private float atkSpeed = 1.0f;
    private MissionSystem mission;
    private float animationTime;
    public override void EnterState(PlayerStateManager pm){
        mission = MissionSystem.GetMission();
        if(mission.GetMissionTracker() == 1){
            mission.AddProgress();
        }
        atkCounter = 1;
        comboChainTime = 2f;
        GameObject.Find("PaladinBasicAtkVoice").GetComponent<AudioSource>().Play();

        animationTime = 1.5f;
        pm.paladinIsAttackAnimation = true;
    }

    public override void UpdateState(PlayerStateManager pm){
        animationTime -= Time.deltaTime;
        if(animationTime < 0){
            pm.paladinIsAttackAnimation = false;
        }
        
        pm.player._animator.SetInteger("atkCounter", atkCounter);
        if(comboChainTime < 0){
            pm.SwapState(pm.idleState);
            return;
        }
        comboChainTime -= Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.Mouse0) && comboChainTime < atkSpeed){
            animationTime = 1.5f;
            comboChainTime = 2f;
            atkCounter++;
            if(mission.GetMissionTracker() == 1){
                mission.AddProgress();
            }
            GameObject.Find("PaladinBasicAtkVoice").GetComponent<AudioSource>().Play();

            if(atkCounter == 3){
                animationTime += 1f;
            }
        }
        if(atkCounter > 3){
            pm.SwapState(pm.idleState);
            return;
        }
    }

    public override void EndState(PlayerStateManager pm){
        atkCounter = 0;
        pm.player._animator.SetInteger("atkCounter", atkCounter);
        return;
    }

    public void SetRageAtkSpeed(){
        this.atkSpeed = 1.5f;
    }

    public void SetNormalAtkSpeed(){
        this.atkSpeed = 1.0f;
    }

}