using UnityEngine;
using UnityEngine.UI;
// settings redo soon to create it on ground. Esc => returning

public class UISettings : MonoBehaviour
{
    [SerializeField] private RectTransform _settingsWindow;

    [SerializeField] private Slider _generalVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private Slider _voiceVolumeSlider;

    // screenResolution, language, etc
}