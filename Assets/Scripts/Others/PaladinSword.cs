using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSword : MonoBehaviour
{
    [SerializeField] private GameObject paladin;
    private PlayerStateManager pm;
    void Start()
    {
        pm = paladin.GetComponent<PlayerStateManager>();
    }

    void Update()
    {
        this.GetComponent<Collider>().enabled = pm.paladinIsAttackAnimation;
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.GetComponent<EnemyStateManager>() != null){
            // Debug.Log("Enemy hitted");
            AudioSource audio = GameObject.Find("SwordHit").GetComponent<AudioSource>();
            audio.Play();
        } else {
            // Debug.Log(other.ToString());
        }
    }
}
