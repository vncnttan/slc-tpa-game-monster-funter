using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlavenusRightHand : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private EnemyStateManager em;
    void Start()
    {
        em = enemy.GetComponent<EnemyStateManager>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider){
        if(collider.GetComponent<PlayerStateManager>() != null){
            if((em.currentState == em.punchAttack || em.currentState == em.bossSwipeAttack)){
                em.currentState.CollidesWithPlayer(em);
            }
            return;
        }
    }
}
