using UnityEngine;

public class WizBasicAtk : MonoBehaviour{
    [SerializeField] private GameObject vfx;
    [SerializeField] private AudioSource sfx;
    private float vfx_timer = 0f;
    private MissionSystem mission;
    void Start(){
        mission = MissionSystem.GetMission();
    }

    void Update(){
        if(vfx_timer > 0){
            vfx.SetActive(true);
            vfx_timer -= Time.deltaTime;
        } else {
            vfx.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(mission.GetMissionTracker() == 1){
                mission.AddProgress();
            }
            vfx_timer = 0.1f;
            sfx.Play();
        }

    }

    public void EndVfx(){
        vfx.SetActive(false);
    }
}