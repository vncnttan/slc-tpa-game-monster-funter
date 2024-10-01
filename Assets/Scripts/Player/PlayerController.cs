// using UnityEngine.UI;
// using System.Collections.Generic;
// using UnityEngine;
// using Cinemachine;

// public class PlayerController : MonoBehaviour
// {
//     public CharacterController controller;
//     public Transform mainCamera;
//     [SerializeField] internal Animator _animator;
//     [SerializeField] internal AudioSource landingSound;
//     [SerializeField] internal List<AudioSource> footsteps;
//     [SerializeField] internal Slider healthSlider;
//     [SerializeField] internal Slider staminaSlider;
//     [SerializeField] private CinemachineFreeLook aimCam;
//     [SerializeField] private CinemachineFreeLook mainCam;
//     [SerializeField] private Image crossHair;
//     private bool isAiming = false;
//     internal static int pref;
//     // 1 - Wizard
//     // 2 - Paladin

//     PlayerParent player;
//     CinemachineFreeLook Cine;

//     void Start()
//     {
//         var Cine = GameObject.Find("ThirdPerson Cinemachine").GetComponent<CinemachineFreeLook>();
//         _animator = GetComponent<Animator>();
//         if(PlayerController.pref != 1){
//             player = new Wizard();
//             if(GameObject.FindGameObjectWithTag("Paladin")){
//                 GameObject.FindGameObjectWithTag("Paladin").SetActive(false);
//             }
//         } else {
//             player = new Paladin();
//             aimCam.gameObject.SetActive(false);
//             crossHair.gameObject.SetActive(false);
//             if(GameObject.FindGameObjectWithTag("Wizard")){
//                 GameObject.FindGameObjectWithTag("Wizard").SetActive(false);
//             }
//         }
//     }

//     // Update is called once per frame
//     // void Update()
//     // {
//     //     if(!isAiming){
//     //         Vector3 direction = player.CharMove(this);
//     //         player. Anim(this, direction);
//     //     }
//     //     player.StaminaRefill(5f * Time.deltaTime);
//     //     player.UpdateCharStats(this);
//     // }

//     void OnLand()
//     {
//         landingSound.Play();
//     }

//     public void SetAimingMode(bool value){
//         isAiming = value;
//         //--XD
//         if(value){
//             aimCam.m_XAxis.Value = mainCam.m_XAxis.Value;
//             aimCam.m_YAxis.Value = mainCam.m_YAxis.Value;
//             controller.transform.rotation = Quaternion.Euler(0f, mainCam.transform.eulerAngles.y, 0f);
//         } else {
//             mainCam.m_XAxis.Value = aimCam.m_XAxis.Value;
//             mainCam.m_YAxis.Value = aimCam.m_YAxis.Value;
//             transform.rotation = Quaternion.Euler(0f, mainCam.transform.eulerAngles.y, 0f);
//         }
//         //--XD
//     }

//     public bool GetAimingMode(){
//         return isAiming;
//     }
// }
