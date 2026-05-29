using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SfxSettingsController : MonoBehaviour
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
            int old_audio_setting = this.audio_setting;
            string entered_text = this.inputField.text;
            int new_audio_setting = 0;

            try {
                new_audio_setting = int.Parse( entered_text );
            
                if (new_audio_setting <   0) new_audio_setting = 0;
                if (new_audio_setting > 100) new_audio_setting = 100;
            } catch {
                new_audio_setting = old_audio_setting;
            }

            this.inputField.text = this.audio_setting.ToString();
            this.audioSlider.value = new_audio_setting;
        }

        public void SliderMoved() {
            this.audio_setting = (int)audioSlider.value;
            this.inputField.text = this.audio_setting.ToString();


            foreach (
                AudioSource source
                in FindObjectsOfType( typeof(AudioSource) )
            )  if (
                new string[] {
                    "Approved",
                    "Denied"
                }
                .Contains(source.name)
            ) {
                source.volume = ( this.audio_setting / 100f );
            }
        }
    // ==--

    void Start()
    {
        this.audioSlider.value = (float)this.audio_setting;
    }
}
