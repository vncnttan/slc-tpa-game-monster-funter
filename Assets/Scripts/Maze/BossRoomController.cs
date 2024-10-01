using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoomController : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    private EnemyStateManager bossmng;
    // Start is called before the first frame update
    void Start()
    {
        bossmng = Boss.GetComponent<EnemyStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossmng.currentState == bossmng.deathState){
            SceneManager.LoadScene("Win");
        }
    }
}
