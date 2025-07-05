using UnityEngine;
using UnityEngine.Audio; using UnityEngine.UI;
public class SoundSettings: MonoBehaviour 
{
    [SerializeField] 
    Slider soundSlider;
    [SerializeField]
    AudioMixer masterMixer;
    private void Start() {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void SetVolume(float _value) {
        if (_value < 1)
        {
            _value = .001f;
        }
        PlayerPrefs.SetFloat("Saved MasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
    }

    public void SetVolumeFromSlider() {
        SetVolume(soundSlider.value);
    }
}