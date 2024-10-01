 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockController
{
    private static DoorLockController doorlock;
    public static bool doorShouldBeLocked;

    private DoorLockController(){
        doorShouldBeLocked = false;
    }

    public static DoorLockController GetDoorLockController(){
        if(doorlock == null){
            doorlock = new DoorLockController();
        }
        return doorlock;
    }

    public void setDoorLock(){
        // SEARCH ALL DOOR AND CLOSE IT IF IT'S OPENED 
        // semua door kasi tag door
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

        foreach(GameObject door in doors) {
            if(door.GetComponent<DoorController>().isOpened){
                LeanTween.rotateY(door, 0, 0.5f);
                door.GetComponent<DoorController>().isOpened = false;
            }
        }
        doorShouldBeLocked = true;
    }

    public void setDoorOpen(){
        doorShouldBeLocked = false;
    }
}
