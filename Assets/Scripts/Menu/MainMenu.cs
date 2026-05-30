using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SettingsMenuToggleVisibility settingsMenuToggleVisibility;

    public void Play()
    {
        SceneManager.LoadScene("UIRedex");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Settings()
    {
        this.settingsMenuToggleVisibility.ToggleVisibility();
    }
}
