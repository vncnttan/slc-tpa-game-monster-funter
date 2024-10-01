using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomController : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;
    DoorLockController doorLockController;
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
        bool allDead = true;
        foreach(EnemyStateManager em in enemiesManager){
            if(em.currentState != em.deathState){
                allDead = false;
            }
        }

        if(allDead){
            doorLockController.setDoorOpen();
        }
    }
}
