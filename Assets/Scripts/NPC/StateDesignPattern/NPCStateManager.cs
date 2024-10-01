using UnityEngine;
using UnityEngine.AI;

public class NPCStateManager : MonoBehaviour
{
    internal NPCBaseState currentState;
    internal NPCIdleState idleState = new NPCIdleState();
    internal NPCLookState lookState = new NPCLookState();
    internal NPCWalkState walkState = new NPCWalkState();

    [SerializeField] private GameObject _npc;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] internal OverlayController overlay;
    [SerializeField] internal GameObject TalkHUD;
    private GameObject mp;

    void Start()
    {
        TalkHUD.SetActive(false);
        idleState.npc = _npc;
        idleState._animator = _animator;
        idleState.agent = _agent;
        if(GameObject.FindGameObjectWithTag("Paladin")){
            mp = GameObject.FindGameObjectWithTag("Paladin");
        } else {
            mp = GameObject.FindGameObjectWithTag("Wizard");
        }
        idleState.player = mp;

        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwapState(NPCBaseState state){
        currentState.EndState(this);
        currentState = state;
        currentState.npc = _npc;
        currentState.player = mp;
        currentState._animator = _animator;
        currentState.agent = _agent;
        currentState.EnterState(this);
    }
}
