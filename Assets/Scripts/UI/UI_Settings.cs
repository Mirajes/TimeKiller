using TMPro;
using UnityEngine;
using UnityEngine.UI;
// settings redo soon to create it on ground. Esc => returning

public class UI_Settings : MonoBehaviour
{
    [SerializeField] private RectTransform _settingsWindow;

    [SerializeField] private Slider _generalVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private Slider _voiceVolumeSlider;

    [SerializeField] private TMP_Text _generalVolumeTMPT;
    [SerializeField] private TMP_Text _musicVolumeTMPT;
    [SerializeField] private TMP_Text _sfxVolumeTMPT;
    [SerializeField] private TMP_Text _voiceVolumeTMPT;

    // screenResolution, language, etc

    private void Awake()
    {
        _generalVolumeSlider.onValueChanged.AddListener(OnGeneralVolumeChange);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChange);
        _voiceVolumeSlider.onValueChanged.AddListener(OnVoiceVolumeChange);
    }

    private void Start()
    {
        //
        Debug.Log(AudioManager.Instance);
        Debug.Log(AudioManager.Instance.MusicVolume);
        SetSliderValue(_generalVolumeSlider, _generalVolumeTMPT, AudioManager.Instance.GeneralVolume);
        SetSliderValue(_musicVolumeSlider, _musicVolumeTMPT, AudioManager.Instance.MusicVolume);
        SetSliderValue(_sfxVolumeSlider, _sfxVolumeTMPT, AudioManager.Instance.SFXVolume);
        SetSliderValue(_voiceVolumeSlider, _voiceVolumeTMPT, AudioManager.Instance.VoiceVolume);
    }

    private void OnDestroy()
    {
        _generalVolumeSlider.onValueChanged.RemoveAllListeners();
        _musicVolumeSlider.onValueChanged.RemoveAllListeners();
        _sfxVolumeSlider.onValueChanged.RemoveAllListeners();
        _voiceVolumeSlider.onValueChanged.RemoveAllListeners();
    }

    private void SetSliderValue(Slider slider, TMP_Text valueField, float value)
    {
        slider.value = value;
        valueField.text = (value * 100).ToString("F2");
    }

    private void OnGeneralVolumeChange(float volume)
    {
        AudioManager.Instance.OnGeneralVolumeChange(volume);
        _generalVolumeTMPT.text = (_generalVolumeSlider.value * 100).ToString("F2");
    }

    private void OnMusicVolumeChange(float volume)
    {
        AudioManager.Instance.OnMusicVolumeChange(volume);
        _musicVolumeTMPT.text = (_musicVolumeSlider.value * 100).ToString("F2");

    }

    private void OnSFXVolumeChange(float volume)
    {
        AudioManager.Instance.OnSFXVolumeChange(volume);
        _sfxVolumeTMPT.text = (_sfxVolumeSlider.value * 100).ToString("F2");

    }

    private void OnVoiceVolumeChange(float volume)
    {
        AudioManager.Instance.OnVoiceVolumeChange(volume);
        _voiceVolumeTMPT.text = (_voiceVolumeSlider.value * 100).ToString("F2");
    }
}