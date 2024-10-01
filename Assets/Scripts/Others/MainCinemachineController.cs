using System.Collections;
using UnityEngine;

public class MainCinemachineController : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Cutscene());
    }
    
    IEnumerator Cutscene(){
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("Start Vcam Cinemachine").SetActive(false);
    }
}
