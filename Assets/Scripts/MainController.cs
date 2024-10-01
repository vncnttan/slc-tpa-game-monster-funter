using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    
    [SerializeField] private static MainController instance;
    
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
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Win") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lose") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Settings")){
            int children = transform.childCount;
            for (int i = 0; i < children; ++i){
                transform.GetChild(i).gameObject.SetActive(false);
            }
        } else {
            int children = transform.childCount;
            for (int i = 0; i < children; ++i){
                if(transform.GetChild(i).gameObject.tag != "LoadingScreen"){
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}
