using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnJPButton()
    {
        SceneManager.LoadScene("JapanChapter");
    }

    public void OnCNButton()
    {
        SceneManager.LoadScene("ChinaChapter");
    }

    public void OnQuitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
