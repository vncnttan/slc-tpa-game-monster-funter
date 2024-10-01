using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardFireSkillScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject WizardObject;
    private PlayerStateManager pm;
    private float fireTimer;
    void Start()
    {
        pm = WizardObject.GetComponent<PlayerStateManager>();
        fireTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay(Collider other){
        if(other.GetComponent<EnemyStateManager>() != null){
            WizardObject = GameObject.FindGameObjectWithTag("Wizard");
            pm = WizardObject.GetComponent<PlayerStateManager>();
            if(pm.currentState == pm.wizardFlame){
                if(fireTimer < 0){
                    EnemyStateManager em = other.GetComponent<EnemyStateManager>();
                    em.enemy.health -= 10;
                    fireTimer = 1f;
                }
                fireTimer -= Time.deltaTime;
            }
        }
    }
}
