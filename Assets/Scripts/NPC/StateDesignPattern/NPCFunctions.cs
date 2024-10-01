using UnityEngine;
public class NPCFunctions
{
    private static float _interactRange = 2f;
    public static bool NPCisNearPlayer(GameObject player, GameObject npc){
        Collider[] colliderArr = Physics.OverlapSphere(npc.transform.position, _interactRange);

        foreach(Collider c in colliderArr){
            if(c.gameObject.tag == "MainChar"){
                return true;
            }
        }
        return false;
    }

}
