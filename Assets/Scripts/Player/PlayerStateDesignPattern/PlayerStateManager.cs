using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using Cinemachine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStateManager : MonoBehaviour
{
    public CharacterController controller;
    public Transform mainCamera;
    [SerializeField] internal AudioSource landingSound;
    [SerializeField] internal List<AudioSource> footsteps;
    [SerializeField] internal Slider healthSlider;
    [SerializeField] internal Slider staminaSlider;
    [SerializeField] internal Slider skill1Slider;
    [SerializeField] internal Slider skill2Slider;
    [SerializeField] internal CinemachineFreeLook aimCam;
    [SerializeField] internal CinemachineFreeLook mainCam;
    internal float speed = 5f;
    [SerializeField] private Image crossHair;
    [SerializeField] private OverlayController overlayController; 
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;
    internal bool isAiming = false;
    private bool win = false;
    internal static int pref = 2;
    internal PlayerBaseState currentState;
    internal PlayerBaseState idleState = new PlayerIdleState();
    internal PlayerBaseState jumpState = new PlayerJumpState();
    internal PlayerBaseState walkState = new PlayerWalkState();
    internal PlayerBaseState runState = new PlayerRunState();
    internal PlayerBaseState dodgeState = new PlayerDodgeState();
    internal PlayerBaseState aimstate = new PlayerAimState();
    internal PlayerBaseState usingItemState = new PlayerUsingItemState();
    internal PlayerBaseState deadState = new PlayerDeadState();
    internal PlayerBaseState damagedState = new PlayerDamagedState();
    internal PlayerBaseState pickUpState = new PlayerPickUpState();
    internal PlayerBaseState wizardFly = new WizardFlyState();
    internal PlayerBaseState wizardFlame = new WizardFlamethrowerState();
    internal PalBasicAtkState paladinatk = new PalBasicAtkState();
    internal PaladinRollState paladinRoll = new PaladinRollState();
    CinemachineFreeLook cine;
    internal PlayerParent player;

    private List<string> _KeystrokeHistory;
    private GameObject WizardObject;
    private GameObject PaladinObject;
    private MissionSystem mission;
    internal bool skill1used = false;
    internal bool skill2used = false;
    private bool isPotionSelected = true;
    private Inventory playerInventory;
    private bool rage = false;
    private bool isLoadingNewScene;
    internal bool paladinIsAttackAnimation = false;

    void Awake(){
        mission = MissionSystem.GetMission();
        _KeystrokeHistory = new List<string>();
        PaladinObject = GameObject.FindGameObjectWithTag("Paladin");
        WizardObject = GameObject.FindGameObjectWithTag("Wizard");
        playerInventory = new Inventory(1, 2);
    }

    void Start()
    {
        cine = GameObject.Find("ThirdPerson Cinemachine").GetComponent<CinemachineFreeLook>();

        Factory paladinFactory = new PaladinFactory();
        Factory wizardFactory = new WizardFactory();

        if(PlayerStateManager.pref == 1){
            // this = new Wizard();
            aimCam.gameObject.SetActive(true);
            crossHair.gameObject.SetActive(true);
            if(PaladinObject){
                PaladinObject.SetActive(false);
                WizardObject.SetActive(true);
            }
            player = wizardFactory.generateCharacter();
        } else {
            // player = new Paladin();
            aimCam.gameObject.SetActive(false);
            crossHair.gameObject.SetActive(false);
            if(WizardObject){
                WizardObject.SetActive(false);
                PaladinObject.SetActive(true);
            }
            player = paladinFactory.generateCharacter();
        }
        player._animator = GetComponent<Animator>();
        currentState = idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        if(isLoadingNewScene){
            return;
        }
        loadingScreen.SetActive(false);
        if(currentState == deadState){
            currentState.UpdateState(this);
            return;
        }
        if(!isAiming && currentState != wizardFlame && currentState != pickUpState && currentState != usingItemState && !rage && currentState != damagedState){
            this.CharMove();
        }
        this.player.StaminaRefill(5f * Time.deltaTime);
        this.player.UpdateCharStats(this);
        this.player.ReduceCooldownWithTime();
        currentState.UpdateState(this);
        CheckCheatCode();
        CheckWinOrLose();
        CheckUseInventory();
        overlayController.UpdateItemQty(playerInventory);
    }

    private void CheckUseInventory(){
        if(Input.GetKeyDown(KeyCode.T)){
            this.overlayController.swapItem();
            isPotionSelected = !isPotionSelected;
        }

        if(Input.GetKeyDown(KeyCode.G)){
            if(isPotionSelected){
                if(playerInventory.potion > 0){
                    playerInventory.UsePotion(player);
                    SwapState(usingItemState);
                }
            } else {
                if(playerInventory.meat > 0){
                    playerInventory.UseMeat(player);
                    SwapState(usingItemState);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.C)){
            Collider[] colliderArr = Physics.OverlapSphere(transform.position, 3f);

            foreach(Collider c in colliderArr){
                if(c.GetComponent<ItemPickable>() != null){
                    if(c.gameObject.name == "Meat"){
                        c.gameObject.SetActive(false);
                        playerInventory.meat += 1;
                        SwapState(pickUpState);
                        break;
                    } else if(c.gameObject.name == "Potion"){
                        c.gameObject.SetActive(false);
                        playerInventory.potion += 1;
                        SwapState(pickUpState);
                        break;
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.J)){
            if(mission.GetMissionTracker() == 4){
                Collider[] colliderArr = Physics.OverlapSphere(transform.position, 1f);

                foreach(Collider c in colliderArr){
                    if(c.name == "Portal"){
                        GoToMaze();
                    }
                }
            }
        }
    }

    private void GoToMaze(){
        isLoadingNewScene = true;
        StartCoroutine(LoadAsynchronously("Maze"));
    }

    IEnumerator LoadAsynchronously(string sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingSlider.value = progress;

            yield return null;
        }
        loadingScreen.SetActive(false);
        isLoadingNewScene = false;
    }

    public void SwapState(PlayerBaseState state){
        currentState.EndState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    void OnLand(){
        landingSound.Play();
    }

    public void CheckState(PlayerBaseState state){
        if(Input.GetKey(KeyCode.Mouse1) && currentState != aimstate && pref == 1){
            SwapState(aimstate);
            return;
        }
        if(Input.GetKey(KeyCode.Mouse0) && currentState != paladinatk && pref == 2) {
            SwapState(paladinatk);
            return;
        }
        if(Input.GetKey(KeyCode.V)){
            SwapState(dodgeState);
            return;
        }
        if(Input.GetKey(KeyCode.R)){
            if(pref == 1 && player.sk1gauge > 99f){
                SwapState(wizardFly);
                return;
            }
            if(pref == 2 && player.sk1gauge > 99f){
                StartCoroutine(PaladinRageSkill());
            }
        }
        if(Input.GetKey(KeyCode.F)){
            if(pref == 1 && player.sk2gauge > 99f){
                SwapState(wizardFlame);
                return;
            }
            if(pref == 2 && player.sk2gauge > 99f){
                SwapState(paladinRoll);
                return;
            }
        }
        if(Input.GetKey(KeyCode.Space) && currentState != jumpState){
            SwapState(jumpState);
            return;
        } else if(direction.magnitude >= 0.1f){
            if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && player.Stamina > 0){
                if(currentState == runState){
                    return;
                } else {
                    SwapState(runState);
                    return;
                }
            } 

            if(currentState != walkState){
                SwapState(walkState);
                return;
            } else {
                return;
            }
        } else {
            if(currentState == paladinatk){
                return;
            }
            SwapState(idleState);
            return;
        }
    }
    
    internal float _gravity = -1.0f;
    public float v_velocity = 2f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector3 direction;
    private Vector3 CharMove(){
        float VerticalAxis = Input.GetAxisRaw("Vertical");
        float HorizontalAxis = Input.GetAxisRaw("Horizontal");

        direction = new Vector3(HorizontalAxis, 0f, VerticalAxis).normalized;
        
        if (this.controller.isGrounded && v_velocity < -0.1f) {
            v_velocity = -1f;
        } else {
            v_velocity += _gravity * Time.deltaTime;
        }
        
        Vector3 moveDirection = new Vector3(0f, 0f, 0f);

        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + this.mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(this.controller.transform.eulerAngles.y, targetAngle, ref this.turnSmoothVelocity, turnSmoothTime);
            this.controller.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        moveDirection.y = v_velocity;
        controller.Move(moveDirection.normalized * this.speed * Time.deltaTime);
        return direction;
    }
    private void CheckWinOrLose(){
        if(this.player.HP <= 0f){
            SwapState(deadState);
        }
        if(this.win == true){
            SceneManager.LoadScene("Win");
        }
    }
    private void CheckCheatCode(){
        KeyCode pressed = DetectKey();
        AddKeyToHistory(pressed.ToString());
        if(GetHistory().ToLower().Contains("a.k.u.s.a.y.a.n.g.a.n.g.k.a.t.a.n.alpha2.alpha2.alpha1")){
            _KeystrokeHistory.Clear();
            this.win = true;
            overlayController.showCheatMsg();
        } else if(GetHistory().ToLower().Contains("i.l.o.v.e.y.o.u")){
            _KeystrokeHistory.Clear();
            player.HP = -1f;
            overlayController.showCheatMsg();
        } else if(GetHistory().ToLower().Contains("i.h.a.t.e.y.o.u")){
            _KeystrokeHistory.Clear();
            this.player.runSpeed *= 2;
            this.player.walkSpeed *= 2;
            overlayController.showCheatMsg();
        } else if(GetHistory().ToLower().Contains("h.e.s.o.y.a.m")){
            _KeystrokeHistory.Clear();
            this.player.sk1gauge = 100f;
            this.player.sk2gauge = 100f;
            overlayController.showCheatMsg();
        } else if(GetHistory().ToLower().Contains("b.u.d.i")){
            _KeystrokeHistory.Clear();
            if(pref == 1){
                pref = 2;
            } else if (pref == 2){
                pref = 1;
            }

            if(PlayerStateManager.pref == 1){
            // this = new Wizard();
                aimCam.gameObject.SetActive(true);
                crossHair.gameObject.SetActive(true);
                if(PaladinObject){
                    PaladinObject.SetActive(false);
                }
                WizardObject.SetActive(true);
                player = new Wizard();
            } else {
                // player = new Paladin();
                aimCam.gameObject.SetActive(false);
                crossHair.gameObject.SetActive(false);
                if(WizardObject){
                    WizardObject.SetActive(false);
                }
                PaladinObject.SetActive(true);
                player = new Paladin();
            }
            player._animator = GetComponent<Animator>();
            overlayController.showCheatMsg();
        } else if(GetHistory().ToLower().Contains("n.o.r.e.v.i.s.i")){
            _KeystrokeHistory.Clear();
            overlayController.showCheatMsg();
            while(mission.GetMissionTracker() != 4){
                mission.ProgressMission();
            }
        }
    }

    

    private KeyCode DetectKey(){
        foreach(KeyCode key in Enum.GetValues(typeof(KeyCode))){
            if(Input.GetKeyDown(key)){
                return key;
            }
        }
        return KeyCode.None;
    }

    private void AddKeyToHistory(string keyStroke){
        if(!keyStroke.Equals("None")){
            _KeystrokeHistory.Add(keyStroke);
            if(_KeystrokeHistory.Count > 50){
                _KeystrokeHistory.RemoveAt(0);
            }
        }
    }

    private string GetHistory(){
        return String.Join('.', _KeystrokeHistory.ToArray());
    }
    
    IEnumerator PaladinRageSkill(){
        player._animator.SetBool("Rage", true);
        player._animator.speed = 1.5f;
        Color color = new Color(0f/255f, 128f/255f, 0f/255f);
        GameObject.Find("RageSound").GetComponent<AudioSource>().Play();
        skill1Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        rage = true;
        yield return new WaitForSeconds(2.4f);
        
        rage = false;
        player._animator.SetBool("Rage", false);
        player.walkSpeed = 10f;
        player.runSpeed = 22f;
        paladinatk.SetRageAtkSpeed();

        yield return new WaitForSeconds(10f);

        player.walkSpeed = 5f;
        player.runSpeed = 12f;
        paladinatk.SetNormalAtkSpeed();
        
        color = new Color(255f/255f, 255f/255f, 255f/255f);
        skill1Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        player.Stamina = 0f;
        player._animator.speed = 1f;
        if(mission.GetMissionTracker() == 2 && this.skill1used == false){
            this.skill1used = true;
            mission.AddProgress();
        }
    }
}
