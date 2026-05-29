using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AudioSettingsController : MonoBehaviour
{
    // --== SERIALISABLE FIELDS ==-- //
        [SerializeField] Slider audioSlider;
        [SerializeField] TMP_InputField inputField;
    // ==--

    // --== FIELDS ==-- //

        public int audio_setting = 25;
    // ==--

    // --== FUNCTIONS --== //

        public void TextFieldEntered() {
            string entered_text = this.inputField.text;
            int new_audio_setting = 0;

            try {
                new_audio_setting = int.Parse( entered_text );
            
                if (new_audio_setting <   0) new_audio_setting = 0;
                if (new_audio_setting > 100) new_audio_setting = 100;
            } catch { }

            this.audioSlider.value = new_audio_setting;
            // Setting input field value is done by the slider which is triggered by the line above
        }

        public void SliderMoved() {
            this.inputField.text = this.audio_setting.ToString();
            this.audio_setting = (int)audioSlider.value;
        }
    // ==--

    void OnStart()
    {
        this.audioSlider.value = (int)this.audio_setting;
    }
}
