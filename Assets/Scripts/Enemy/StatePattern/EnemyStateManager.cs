using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStateManager : MonoBehaviour{
    internal EnemyBaseState currentState;
    internal EnemyBaseState idleState = new EnemyIdleState();
    internal EnemyBaseState chaseState = new EnemyChaseState();
    internal GameObject player;
    internal GameObject enemyobject;
    internal NavMeshAgent agent;
    internal Animator animator;
    internal Enemy enemy;

    internal EnemyBaseState fireBreath = new FireBreathState();
    internal EnemyBaseState jumpAttack = new JumpAttackState();
    internal EnemyBaseState punchAttack = new PunchAttackState();
    internal EnemyBaseState bossSwipeAttack = new SwipeAttackState();
    internal EnemyBaseState deathState = new EnemyDeathState();
    internal EnemyBaseState swipe1 = new Swipe1State();
    internal EnemyBaseState swipe2 = new Swipe2State();
    internal EnemyBaseState swipe3 = new Swipe3State();
    
    internal List<EnemyBaseState> availableAtkStates;
    [SerializeField] Slider HPBar;

    void Start(){
        currentState = idleState;
        if(GameObject.FindGameObjectWithTag("Paladin")){
            player = GameObject.FindGameObjectWithTag("Paladin");
        } else {
            player = GameObject.FindGameObjectWithTag("Wizard");
        }
        enemyobject = this.gameObject;
        agent = enemyobject.GetComponent<NavMeshAgent>();
        animator = enemyobject.GetComponent<Animator>();
        
        if(enemyobject.name == "Glavenus"){
            enemy = new Glavenus();
            availableAtkStates = new List<EnemyBaseState>{
                fireBreath,
                jumpAttack,
                punchAttack,
                bossSwipeAttack
            };
        } else if (enemyobject.name == "Gammoth"){
            enemy = new Gammoth();
            availableAtkStates = new List<EnemyBaseState>{
                swipe1,
                swipe2,
                swipe3
            };
        }
        agent.stoppingDistance = enemy.atkRange;

        currentState.EnterState(this);
    }
    
    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == 9){
            enemy.health -= 10;
        }
    }

    void Update(){
        // Debug.Log(currentState.ToString()  + enemyobject.name);
        updateHealth();
        healthBarLookAt();
        if(currentState != deathState){
            ValidateDeath();
        }
        currentState.UpdateState(this);
    }

    public void SwapState(EnemyBaseState state){
        currentState.EndState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void updateHealth(){
        HPBar.value = (enemy.health/enemy.maxHealth)*100;
    }

    public void healthBarLookAt(){
        Vector3 lookRotate = new Vector3(player.transform.position.x - HPBar.gameObject.transform.position.x, 0f, player.transform.position.z - HPBar.gameObject.transform.position.z);
        Quaternion lookRotation = Quaternion.LookRotation(lookRotate);
        HPBar.gameObject.transform.rotation = Quaternion.RotateTowards(HPBar.gameObject.transform.rotation, lookRotation, 500f * Time.deltaTime);
    }

    public void ValidateDeath(){
        if(enemy.health < 0f){
            SwapState(deathState);
        }
        return;
    }
}