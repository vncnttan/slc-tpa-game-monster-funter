using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToVillage(){
        SceneManager.LoadScene("Temp");
    }
    public void BackToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
