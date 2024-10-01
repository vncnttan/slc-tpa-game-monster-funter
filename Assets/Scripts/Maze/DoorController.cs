using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    internal bool isOpened = false;
    private DoorLockController doorLockController;
    void Start()
    {
        isOpened = false;
        doorLockController = DoorLockController.GetDoorLockController();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)){
            Collider[] colliderArr = Physics.OverlapSphere(this.transform.position, 2f);

            foreach(Collider c in colliderArr){
                if(c.gameObject.tag == "MainChar" && !DoorLockController.doorShouldBeLocked){
                    Debug.Log(DoorLockController.doorShouldBeLocked);
                    if(isOpened){
                        LeanTween.rotateY(gameObject, 0, 0.5f);
                        isOpened = false;
                    } else {
                        LeanTween.rotateY(gameObject, 90, 0.5f);
                        isOpened = true;
                    }
                }
            }
        }
    }
}
