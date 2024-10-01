using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private CinemachineVirtualCamera currentCamera;
    public void OpenSettings()
    {
        LevelLoad.SwapScene("Settings");
    }
    public void OpenMainMenu()
    {
        LevelLoad.SwapScene("MainMenu");
    }
    public void UpdateCam(CinemachineVirtualCamera newCam)
    {
        currentCamera.Priority -= 6;
        currentCamera = newCam;
        currentCamera.Priority += 6;
    }
    public void ChangeCanvas(bool showCanvas)
    {
        canvas.SetActive(showCanvas);
    }

}
