using System.Collections.Generic;
using UnityEngine;

public class NPCLookState : NPCBaseState
{
    private float _speed = 600.0f;
    private static float _interactRange = 5f;
    private bool isShowingDialog = false;
    private bool DarianQuested = false;
    private bool CesiyaQuested = false;
    private MissionSystem mission;
    public override void EnterState(NPCStateManager npcmanager){
        isShowingDialog = false;
        npcmanager.overlay.StopCharacterDialog();
        mission = MissionSystem.GetMission();
        npcmanager.TalkHUD.SetActive(true);
    }

    public override void UpdateState(NPCStateManager npcmanager){
        Vector3 npcPos = this.npc.transform.position;
        Vector3 playerPos = this.player.transform.position;

        Vector3 delta = new Vector3(playerPos.x - npcPos.x, 0.0f, playerPos.z - npcPos.z);
        
        Quaternion rotation = Quaternion.LookRotation(delta);

        this.npc.transform.rotation = Quaternion.RotateTowards(this.npc.transform.rotation, rotation, _speed * Time.deltaTime);

        if(!NPCFunctions.NPCisNearPlayer(this.player, this.npc)){
            npcmanager.SwapState(npcmanager.idleState);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            Collider[] colliderArr = Physics.OverlapSphere(npc.transform.position, _interactRange);

            bool NPCCollidedWithPlayer = false;

            foreach(Collider c in colliderArr){
                if(c.gameObject.tag == "MainChar"){
                    NPCCollidedWithPlayer = true;
                    Dialogue.CheckQueue();

                    if(isShowingDialog){
                        break;
                    }

                    if(this.npc.name == "Lyra"){
                        if(mission.GetMissionTracker() == 0 || mission.GetMissionTracker() == 3){
                            mission.AddProgress();
                        }

                        if(mission.isMissionCompleted()){
                            mission.ProgressToNextMission();
                            npcmanager.overlay.ProgressToNextMission();
                            npcmanager.overlay.ShowCharacterDialog(mission.GetMissionDesc());
                        } else {
                            npcmanager.overlay.ShowCharacterDialog(Dialogue.LyraHintDialogue[mission.GetMissionTracker()]);
                        }
                        isShowingDialog = true;
                    } else if (this.npc.name == "Darian"){
                        if(mission.GetMissionTracker() == 3 && DarianQuested == false){
                            mission.AddProgress();
                            DarianQuested = true;
                        }
                        npcmanager.overlay.ShowCharacterDialog(Dialogue.DarianDialogue.Dequeue());
                        isShowingDialog = true;
                    } else if(this.npc.name == "Cesiya"){
                        if(mission.GetMissionTracker() == 3 && CesiyaQuested == false){
                            mission.AddProgress();
                            CesiyaQuested = true;
                        }
                        npcmanager.overlay.ShowCharacterDialog(Dialogue.CesiyaDialogue.Dequeue());
                        isShowingDialog = true;
                    }
                }
            }

            if(!NPCCollidedWithPlayer && isShowingDialog){
                npcmanager.overlay.StopCharacterDialog();
                isShowingDialog = false;
            }
        }
    }
    public override void EndState(NPCStateManager npcmanager){
        npcmanager.overlay.StopCharacterDialog();
        isShowingDialog = false;
        npcmanager.TalkHUD.SetActive(false);
    }
}
