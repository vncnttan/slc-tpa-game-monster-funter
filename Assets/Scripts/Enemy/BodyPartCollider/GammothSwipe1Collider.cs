using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammothSwipe1Collider : MonoBehaviour
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
            if(em.currentState == em.swipe1 ){
                em.currentState.CollidesWithPlayer(em);
            }
            return;
        }
    }
}
