using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WizardProjectile : MonoBehaviour
{
    private Rigidbody bulletRigid;
    private void Awake(){
        bulletRigid = GetComponent<Rigidbody>(); 
    }

    private void Start(){
        float speed = 100f;
        bulletRigid.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other){
        if(other.GetComponent<EnemyStateManager>() != null){
            // Debug.Log("Enemy hitted");
            AudioSource audio = GameObject.Find("Bullethit").GetComponent<AudioSource>();
            audio.Play();
        } else {
            // Bukan Enemy
        }
        Destroy(gameObject);
    }
}
