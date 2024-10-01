using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OverlayController : MonoBehaviour
{
    [SerializeField] GameObject MissionSuccessAnnounce;
    [SerializeField] GameObject CheatAnnounce;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] TextMeshProUGUI Dialogtxt;
    [SerializeField] TextMeshProUGUI Missiontxt;
    [SerializeField] GameObject DialoguePanel;
    [SerializeField] GameObject ItemSet;
    [SerializeField] TextMeshProUGUI meatQtyTxt;
    [SerializeField] TextMeshProUGUI potionQtyTxt;
    private bool isPaused = false;
    private float MissionSuccessTimer;
    MissionSystem missionActive;
    void Start()
    {
        missionActive = MissionSystem.GetMission();
        PauseScreen.SetActive(false);
        CheatAnnounce.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                Resume();
            } else {
                Pause();
            }
        }

        Missiontxt.text = missionActive.GetMissionDesc() + "(" + missionActive.GetCurrentProgress() + "/" + missionActive.GetCurrentGoalProgress() + ")";
        if(missionActive.isMissionCompleted()){
            Missiontxt.color = new Color(0/255f, 255f/255f, 0/255f, 1);
        } else {
            Missiontxt.color = new Color(255f/255f, 255f/255f, 255f/255f, 1);
        }
    }
    public void Resume(){
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        isPaused = false;
    }
    void Pause(){
        isPaused = true;
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator ShowCheat(){
        CheatAnnounce.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        CheatAnnounce.SetActive(false);
    }

    public void PauseBackToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void showCheatMsg(){
        StartCoroutine(ShowCheat());
    }

    public void ShowCharacterDialog(string dialog){
        StopCharacterDialog();
        DialoguePanel.SetActive(true);
        StartCoroutine(TypeSentence(dialog));
    }
    public void StopCharacterDialog(){
        StopCoroutine(TypeSentence(""));
        DialoguePanel.SetActive(false);
        Dialogtxt.text = "";
    }
    public void ProgressToNextMission(){
        StartCoroutine(ShowMissionSuccess());
    }
    IEnumerator TypeSentence(string sentence){
        Dialogtxt.text = "";
        foreach(char letter in sentence.ToCharArray()){
            Dialogtxt.text += letter;
            yield return null;
        }
    }
    IEnumerator ShowMissionSuccess(){
        MissionSuccessAnnounce.SetActive(true);
        yield return new WaitForSeconds(3f);
        MissionSuccessAnnounce.SetActive(false);
    }
    
    public void swapItem(){
        Vector3 pos = ItemSet.transform.localPosition;
        pos.x = pos.x * -1;
        // Debug.Log(pos.x);

        ItemSet.transform.localPosition = pos;
    }
    public void UpdateItemQty(Inventory inv){
        meatQtyTxt.text = inv.meat.ToString();
        potionQtyTxt.text = inv.potion.ToString();
    }
}
