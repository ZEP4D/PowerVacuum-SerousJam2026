using UnityEngine;

public class SettingsMenuToggleVisibility : MonoBehaviour
{
    [SerializeField] Canvas settings_menu_canvas;
    
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) ) {
            settings_menu_canvas.enabled = !settings_menu_canvas.enabled;
        }
    }
}
