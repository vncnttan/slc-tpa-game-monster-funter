using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HoverMenuCharacterController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource voice;
    [SerializeField] private GameObject spotlight;
    [SerializeField] private GameObject particle;

    // Loading Screen
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;
    void Start()
    {
        spotlight.SetActive(false);
        particle.SetActive(false);
    }

    void OnMouseEnter()
    {
        // Debug.Log("Hovered!");
        voice.Play();
        // Debug.Log(spotlight);
        spotlight.SetActive(true);
        _animator.SetBool("isHovered", true);
        particle.SetActive(true);
    }

    void OnMouseExit()
    {
        spotlight.SetActive(false);
        _animator.SetBool("isHovered", false);
        particle.SetActive(false);
    }

    private void OnMouseDown(){
        if(this.name == "Paladin J Nordstrom"){
            PlayerStateManager.pref = 2;
        } else {
            PlayerStateManager.pref = 1;
        }
        LoadLevel(3);
    }

    public void LoadLevel(int sceneIndex){
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone){
            // Debug.Log(operation.progress);
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingSlider.value = progress;

            yield return null;
        }
    }
}
