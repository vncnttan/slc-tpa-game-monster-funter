// using UnityEngine;

// public abstract class PlayerBase {
//     // internal float walkSpeed;
//     // internal float runSpeed;

//     // internal float Stamina;
//     // internal float HP;
//     // public void UpdateCharStats(PlayerController con){
//     //     con.healthSlider.value = this.HP;
//     //     con.staminaSlider.value = this.Stamina;
//     // }

//     // public virtual void StaminaRefill(float value){
//     //     this.Stamina += value % 101;
//     // }

//     // Footstep Sound
//     // public virtual void FootstepManager(PlayerController con, Vector3 direction){
//     //     if(speed == this.runSpeed){
//     //         con.footsteps[Random.Range(0, 9)].Play();
//     //         footstepDelayTime = 0.3f; 
//     //     } else {
//     //         con.footsteps[Random.Range(0, 9)].Play();
//     //         footstepDelayTime = 0.7f;
//     //     } 
//     // }

//     // Character Animation
//     public virtual void CharAnim(PlayerController con, Vector3 direction){
//         // con._animator.SetBool("Jump", false);
//         // con._animator.SetBool("Walking", false);
//         // con._animator.SetBool("Run", false);
//         // con._animator.SetBool("Grounded", false);

//         // if(Input.GetKey(KeyCode.Space)){
//         //     con._animator.SetBool("Jump", true);
//         // } else if (direction.magnitude >= 0.1f) {
//         //     if(speed == this.runSpeed){
//         //         con._animator.SetBool("Run", true);
//         //     } 
//         //     con._animator.SetBool("Walking", true);
            
//         //     if(footstepDelayTime < 0 && con.controller.isGrounded){
//         //         FootstepManager(con, direction);
//         //     }

//         //     footstepDelayTime -= Time.deltaTime;
//         // }
        
//         // if (con.controller.isGrounded){
//         //     con._animator.SetBool("Grounded", true);
//         // }
//     }

//     // Character Movement
//     private static float _gravity = -1.0f;
//     private static float _velocity;
//     private static float speed = 5f;

//     private static float turnSmoothTime = 0.1f;
//     private static float turnSmoothVelocity;
//     private static float footstepDelayTime = 0.7f;

//     // public virtual Vector3 CharMove(PlayerController con){
//         // float VerticalAxis = Input.GetAxisRaw("Vertical");
//         // float HorizontalAxis = Input.GetAxisRaw("Horizontal");

//         // Vector3 direction = new Vector3(HorizontalAxis, 0f, VerticalAxis).normalized;

//         // if (con.controller.isGrounded && _velocity < -0.05f) {
//         //     _velocity = -1f;
//         //     if (Input.GetKey(KeyCode.Space))
//         //     {
//         //         _velocity = 0.8f;
//         //     }
//         // }
//         // else {
//         //     _velocity += _gravity * Time.deltaTime;
//         // }

//         // Vector3 moveDirection = new Vector3(0f, 0f, 0f);

//         // if (direction.magnitude >= 0.1f) {
//         //     float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + con.mainCamera.eulerAngles.y;
//         //     float angle = Mathf.SmoothDampAngle(con.controller.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
//         //     con.controller.transform.rotation = Quaternion.Euler(0f, angle, 0f);
//         //     moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
//         // }

//         // moveDirection.y = _velocity;

//         // if (Input.GetKey(KeyCode.LeftShift) && this.Stamina > 0.0f && con.controller.isGrounded){
//         //     speed = this.runSpeed;
//         //     this.Stamina -= 10f * Time.deltaTime;
//         // } else {
//         //     speed = this.walkSpeed;
//         // }
        
//         // con.controller.Move(moveDirection.normalized * speed * Time.deltaTime);
//         // return direction;
//     // }
// }