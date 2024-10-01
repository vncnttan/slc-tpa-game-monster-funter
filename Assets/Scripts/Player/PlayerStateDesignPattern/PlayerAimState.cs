using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAimState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager pm){
        pm.aimCam.m_XAxis.Value = pm.mainCam.m_XAxis.Value;
        pm.aimCam.m_YAxis.Value = pm.mainCam.m_YAxis.Value;
        pm.controller.transform.rotation = Quaternion.Euler(0f, pm.mainCam.transform.eulerAngles.y, 0f);
        pm.GetComponent<RigBuilder>().enabled = true;
        pm.GetComponent<WizBasicAtk>().enabled = true;
    }

    public override void UpdateState(PlayerStateManager pm){
        if(!Input.GetKey(KeyCode.Mouse1)){
            pm.SwapState(pm.idleState);
        }
    }

    public override void EndState(PlayerStateManager pm){
        pm.mainCam.m_XAxis.Value = pm.aimCam.m_XAxis.Value;
        pm.mainCam.m_YAxis.Value = pm.aimCam.m_YAxis.Value;
        pm.transform.rotation = Quaternion.Euler(0f, pm.mainCam.transform.eulerAngles.y, 0f);
        pm.GetComponent<RigBuilder>().enabled = false;
        pm.GetComponent<WizBasicAtk>().enabled = false;
        
    }

    
}