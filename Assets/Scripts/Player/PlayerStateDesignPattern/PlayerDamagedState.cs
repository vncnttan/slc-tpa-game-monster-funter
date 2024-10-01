using UnityEngine;
public class PlayerDamagedState : PlayerBaseState{
    private float timer;
    public override void EnterState(PlayerStateManager pm){
        pm.player._animator.SetTrigger("Damaged");
        timer = 2.7f;
    }

    public override void UpdateState(PlayerStateManager pm){
        timer -= Time.deltaTime;
        if(timer < 0.5f){
            
            pm.controller.center = new Vector3(0, -0.8f, 0);
        }else if(timer < 2.5f){
            pm.controller.center = new Vector3(0, -0f, 0);
        }
        CharMove(pm);
        if(timer < 0){
            pm.SwapState(pm.idleState);
        }
    }


    public override void EndState(PlayerStateManager pm){

    }

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private void CharMove(PlayerStateManager pm){
        Vector3 direction = new Vector3(0f, 0f, 0f).normalized;
        
        if (pm.controller.isGrounded && pm.v_velocity < -0.1f) {
            pm.v_velocity = -1f;
        } else {
            pm.v_velocity += pm._gravity * Time.deltaTime;
        }
        
        Vector3 moveDirection = new Vector3(0f, 0f, 0f);

        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + pm.mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(pm.controller.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            pm.controller.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        moveDirection.y = pm.v_velocity;
        pm.controller.Move(moveDirection.normalized * pm.speed * Time.deltaTime);
        return;
    }
}