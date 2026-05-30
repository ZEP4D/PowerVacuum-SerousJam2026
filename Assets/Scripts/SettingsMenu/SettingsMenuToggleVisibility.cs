using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuToggleVisibility : MonoBehaviour
{
    [SerializeField] Canvas settings_menu_canvas;
    
    void Update() {

        if ( Input.GetKeyDown(KeyCode.Escape) ) {
            this.ToggleVisibility();
        }
    }

    public void ToggleVisibility() {
       settings_menu_canvas.enabled = !settings_menu_canvas.enabled;
    }

    public void ReturnToMenuPressed() {
        Debug.Log("See you later :3");
        SceneManager.LoadScene("MainMenu");
    }
}
