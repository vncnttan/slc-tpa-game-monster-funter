using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] private static BackGroundMusic instance;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioClip MainMenuMusic;
    [SerializeField] private AudioClip VillageMusic;
    [SerializeField] private AudioClip OutsideVillageMusic;
    [SerializeField] private AudioClip DungeonMusic;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update(){
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu")){
            if(bgm.clip != MainMenuMusic){
                bgm.Stop();
                bgm.clip = MainMenuMusic;
                bgm.Play();
            }
        }
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Village")){
            if(bgm.clip != VillageMusic && GameObject.Find("Char").transform.position.z < 18 && GameObject.Find("Char").transform.position.x < 20){
                // Debug.Log(GameObject.Find("Char").transform.position.x);
                bgm.Stop();
                bgm.clip = VillageMusic;
                bgm.Play();
            } else if (bgm.clip != OutsideVillageMusic && (GameObject.Find("Char").transform.position.z >= 18 || GameObject.Find("Char").transform.position.x >= 20)){
                bgm.Stop();
                bgm.clip = OutsideVillageMusic;
                bgm.Play();
            }
        }
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Maze")){
            if(bgm.clip != DungeonMusic){
                bgm.Stop();
                bgm.clip = DungeonMusic;
                bgm.Play();
            }
        }
    }
}