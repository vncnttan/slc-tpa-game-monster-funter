
using UnityEngine;
using UnityEngine.Audio;

public class SettingController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MusicVol", volume);
    }

    public void SetAudio(float volume)
    {
        audioMixer.SetFloat("AudioVol", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool choice)
    {
        Screen.fullScreen = choice;
    }
}
