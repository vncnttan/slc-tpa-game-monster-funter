using UnityEngine;
using UnityEngine.Audio;

public class PalBasicAtk : MonoBehaviour{
    internal AudioSource palatksfx;
    void Start(){
        palatksfx = new AudioSource();
        palatksfx = GameObject.Find("PaladinBasicAtkVoice").GetComponent<AudioSource>();
    }

    void Update(){


    }

    // public void PlaySfx(){
    //     palatksfx.Play();
    // }
}