using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomTriggerController : MonoBehaviour
{
    DoorLockController doorLockController;
    [SerializeField] List<GameObject> enemies;
    List<EnemyStateManager> enemiesManager;
    void Start()
    {
        enemiesManager = new List<EnemyStateManager>();
        foreach(GameObject enemy in enemies){
            enemiesManager.Add(enemy.GetComponent<EnemyStateManager>());
        }
        doorLockController = DoorLockController.GetDoorLockController();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "MainChar"){
            foreach(EnemyStateManager em in enemiesManager){
                em.SwapState(em.chaseState);
            }
            doorLockController.setDoorLock();
            this.gameObject.SetActive(false);
        }
    }
}
