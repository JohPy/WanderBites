using UnityEngine;
using UnityEngine.SceneManagement;

public class EndscreenButtons : MonoBehaviour
{
    public void OnQuitToMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuitToDesktopButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}