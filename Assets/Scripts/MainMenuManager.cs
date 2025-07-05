using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    private void UpdateSensitivity()
    {
        sensitivitySlider.value = SaveManager.Instance.mouseSensitivity;
    }
    private void UpdateVolume()
    {
        volumeSlider.value = SaveManager.Instance.volume;
    }

    public void SensitivityChange()
    {
        SaveManager.Instance.mouseSensitivity = sensitivitySlider.value;
    }
    public void VolumeChange()
    {
        SaveManager.Instance.volume = volumeSlider.value;
    }

    public void GoToLevel()
    {
        SaveManager.Instance.GoToLevel();
    }
    public void QuitGame()
    {
        SaveManager.Instance.QuitGame();
    }


    private void Start()
    {
        UpdateSensitivity();
        UpdateVolume();
    }
}
