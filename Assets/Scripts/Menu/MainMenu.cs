using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("DocumentNewLookScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
