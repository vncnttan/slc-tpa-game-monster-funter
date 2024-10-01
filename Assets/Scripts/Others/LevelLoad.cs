using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    public static void SwapScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
