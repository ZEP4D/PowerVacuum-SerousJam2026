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
                new_audio_setting = Mathf.Clamp(
                    int.Parse( entered_text ),
                    0,
                    100
                );
            } catch {
                new_audio_setting = old_audio_setting;
            }

            this.inputField.text = this.audio_setting.ToString();
            this.audioSlider.value = new_audio_setting;
        }

        public void SliderMoved() {
            this.audio_setting = (int)audioSlider.value;
            this.inputField.text = this.audio_setting.ToString();

            PlayerPrefs.SetInt("sfx_setting", this.audio_setting);

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

    void Start() {
        this.audioSlider.value = PlayerPrefs.GetInt(
            "sfx_setting", this.audio_setting
        );
    }
}
