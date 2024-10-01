using UnityEngine;
using UnityEngine.UI;

public class WizardFlamethrowerState : PlayerBaseState
{
    private float fireTime = 2f;
    private ParticleSystem FireVFX;
    private MissionSystem mission;
    public override void EnterState(PlayerStateManager pm){
        mission = MissionSystem.GetMission();
        Color color = new Color(0f/255f, 128f/255f, 0f/255f);
        pm.skill2Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;

        FireVFX = GameObject.Find("Flamethrower").GetComponent<ParticleSystem>();
        GameObject.Find("FlamethrowerSound").GetComponent<AudioSource>().Play();
        
        pm.player._animator.SetBool("Fire", true);
        fireTime = 4f;
    }

    public override void UpdateState(PlayerStateManager pm){
        fireTime -= Time.deltaTime;
        if(fireTime < 0){
            pm.SwapState(pm.idleState);
        }
        if(fireTime < 3f && !FireVFX.isPlaying){
            FireVFX.Play();
        }
    }

    public override void EndState(PlayerStateManager pm){
        pm._gravity = -1.0f;
        FireVFX.Stop();
        pm.player._animator.SetBool("Fire", false);
        pm.player.sk2gauge = 0f;
        
        Color color = new Color(255f/255f, 255f/255f, 255f/255f);
        pm.skill2Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        if(mission.GetMissionTracker() == 2 && pm.skill2used == false){
            pm.skill2used = true;
            mission.AddProgress();
        }
    }

}