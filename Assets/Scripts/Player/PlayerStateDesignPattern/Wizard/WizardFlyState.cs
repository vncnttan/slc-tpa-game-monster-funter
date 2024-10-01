using UnityEngine;
using UnityEngine.UI;

public class WizardFlyState : PlayerBaseState
{
    private float flyTime;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float FlySpeed = 20f;
    private MissionSystem mission;
    public override void EnterState(PlayerStateManager pm){
        mission = MissionSystem.GetMission();
        Color color = new Color(0f/255f, 128f/255f, 0f/255f);
        pm.skill1Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;

        pm.v_velocity = 50.0f;
        pm.player._animator.SetBool("Fly", true);
        flyTime = 5f;
        GameObject.Find("FlyingSound").GetComponent<AudioSource>().Play();
    }
    public override void UpdateState(PlayerStateManager pm){
        if(flyTime < 4.0f){
            pm._gravity = 0f;
            pm.v_velocity = 0f;
        }
        flyTime -= Time.deltaTime;
        if(flyTime < 0){
            pm.SwapState(pm.idleState);
        }

        Vector3 direction = new Vector3(0f, 0f, 1f).normalized;        
        Vector3 moveDirection = new Vector3(0f, 0f, 0f);
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + pm.mainCamera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(pm.controller.transform.eulerAngles.y, targetAngle, ref this.turnSmoothVelocity, this.turnSmoothTime);
        pm.controller.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        pm.controller.Move(moveDirection.normalized * this.FlySpeed * Time.deltaTime);
    }

    public override void EndState(PlayerStateManager pm){
        pm._gravity = -1.0f;
        pm.player._animator.SetBool("Fly", false);
        pm.player.sk1gauge = 0f;
        Color color = new Color(255f/255f, 255f/255f, 255f/255f);
        pm.skill1Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        if(mission.GetMissionTracker() == 2 && pm.skill1used == false){
            pm.skill1used = true;
            mission.AddProgress();
        }
    }

}