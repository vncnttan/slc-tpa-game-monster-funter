using UnityEngine;
using UnityEngine.UI;

public class PaladinRollState : PlayerBaseState
{
    private float rollTime;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float RollSpeed = 8f;
    private MissionSystem mission;
    public override void EnterState(PlayerStateManager pm){
        mission = MissionSystem.GetMission();
        Color color = new Color(0f/255f, 128f/255f, 0f/255f);
        pm.skill2Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;

        pm.player._animator.SetBool("Roll", true);
        rollTime = 1.5f;
        GameObject.Find("RollingSound").GetComponent<AudioSource>().Play();
    }
    public override void UpdateState(PlayerStateManager pm){
        rollTime -= Time.deltaTime;
        if(rollTime < 0){
            pm.SwapState(pm.idleState);
        }
        if(rollTime < 0.1f){
            pm.controller.center = new Vector3(0, -0.8f, 0);
        }else if(rollTime < 0.8f){
            pm.controller.center = new Vector3(0, -0f, 0);
        }

        if(rollTime < 1f){
            Vector3 direction = Quaternion.Euler(0, 0, 1f) * pm.transform.forward;
            pm.controller.Move(direction * this.RollSpeed * Time.deltaTime);
        }
    }

    public override void EndState(PlayerStateManager pm){
        pm._gravity = -1.0f;
        pm.player._animator.SetBool("Roll", false);
        pm.player.sk2gauge = 0f;
        Color color = new Color(255f/255f, 255f/255f, 255f/255f);
        pm.skill2Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        if(mission.GetMissionTracker() == 2 && pm.skill2used == false){
            pm.skill2used = true;
            mission.AddProgress();
        }
    }

}