using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlavenusFireCollider : MonoBehaviour
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
    void OnTriggerStay(Collider collider){
        if(collider.GetComponent<PlayerStateManager>() != null){
            if(em.currentState == em.fireBreath){
                em.currentState.CollidesWithPlayer(em);
            }
            return;
        }
    }
}
