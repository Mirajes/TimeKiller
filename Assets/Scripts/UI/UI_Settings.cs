using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// settings redo soon to create it on ground. Esc => returning
// maybe setting anchor position by object will be better
public class UI_Settings : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private UI_TVController _tvOpener;

    [Header("DOTween")]
    [SerializeField] private float _onOffTime = 1f;
    [SerializeField] private Vector2 _anchorPosition;

    [Header("Buttons")]
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _audioWindowButton;
    [SerializeField] private Button _otherWindowButton;

    [Header("Audio")]
    [SerializeField] private RectTransform _audioWindow;
    [SerializeField] private Slider _generalVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private Slider _voiceVolumeSlider;
    [SerializeField] private TMP_Text _generalVolumeTMPT;
    [SerializeField] private TMP_Text _musicVolumeTMPT;
    [SerializeField] private TMP_Text _sfxVolumeTMPT;
    [SerializeField] private TMP_Text _voiceVolumeTMPT;

    [Header("Other")]
    [SerializeField] private bool _isCrashPopUpActive = false;

    // screenResolution, language, etc

    private void Awake()
    {
        InitVolumeSliders();

        _exitButton.onClick.AddListener(Close);
    }

    private void Start()
    {
        InitValueVolumeSliders();
    }

    private void OnDestroy()
    {
        DeInitVolumeSliders();
        _exitButton.onClick.RemoveAllListeners();

        DOTween.Kill(_rectTransform);
    }

    public void Init()
    {
        _anchorPosition = _rectTransform.anchoredPosition;
    }

    public void SetTVOpener(UI_TVController tvController)
    {
        _tvOpener = tvController;
    }

    public void Open()
    {
        DOTween.Kill(_rectTransform);

        _rectTransform.gameObject.SetActive(false);

        float rectHeight = _rectTransform.rect.height;

        Vector2 startPosition = new Vector2(
            _anchorPosition.x,
            -rectHeight
            );

        _rectTransform.anchoredPosition = startPosition;

        _rectTransform.gameObject.SetActive(true);

        _rectTransform.DOAnchorPos(_anchorPosition, _onOffTime);
    }

    public void Close()
    {
        DOTween.Kill(_rectTransform);

        float rectHeight = _rectTransform.rect.height;

        Vector2 endPosition = new Vector2(
            _anchorPosition.x,
            -rectHeight
            );

        _rectTransform.DOAnchorPos(endPosition, _onOffTime).OnComplete(() =>
        {
            _rectTransform.gameObject.SetActive(false);
            _tvOpener.Open();
        });
    }

    #region Audio
    private void InitVolumeSliders()
    {
        _generalVolumeSlider.onValueChanged.AddListener(OnGeneralVolumeChange);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChange);
        _voiceVolumeSlider.onValueChanged.AddListener(OnVoiceVolumeChange);

    }

    private void InitValueVolumeSliders()
    {
        SetSliderValue(_generalVolumeSlider, _generalVolumeTMPT, AudioManager.Instance.GeneralVolume);
        SetSliderValue(_musicVolumeSlider, _musicVolumeTMPT, AudioManager.Instance.MusicVolume);
        SetSliderValue(_sfxVolumeSlider, _sfxVolumeTMPT, AudioManager.Instance.SFXVolume);
        SetSliderValue(_voiceVolumeSlider, _voiceVolumeTMPT, AudioManager.Instance.VoiceVolume);
    }

    private void DeInitVolumeSliders()
    {
        _generalVolumeSlider.onValueChanged.RemoveAllListeners();
        _musicVolumeSlider.onValueChanged.RemoveAllListeners();
        _sfxVolumeSlider.onValueChanged.RemoveAllListeners();
        _voiceVolumeSlider.onValueChanged.RemoveAllListeners();
    }

    private void OpenAudioWindow()
    {

    }

    private void SetSliderValue(Slider slider, TMP_Text valueField, float value)
    {
        slider.value = value;
        valueField.text = (value * 100).ToString("F0");
    }

    private void OnGeneralVolumeChange(float volume)
    {
        AudioManager.Instance.OnGeneralVolumeChange(volume);
        _generalVolumeTMPT.text = (_generalVolumeSlider.value * 100).ToString("F0");
    }
    private void OnMusicVolumeChange(float volume)
    {
        AudioManager.Instance.OnMusicVolumeChange(volume);
        _musicVolumeTMPT.text = (_musicVolumeSlider.value * 100).ToString("F0");

    }
    private void OnSFXVolumeChange(float volume)
    {
        AudioManager.Instance.OnSFXVolumeChange(volume);
        _sfxVolumeTMPT.text = (_sfxVolumeSlider.value * 100).ToString("F0");

    }
    private void OnVoiceVolumeChange(float volume)
    {
        AudioManager.Instance.OnVoiceVolumeChange(volume);
        _voiceVolumeTMPT.text = (_voiceVolumeSlider.value * 100).ToString("F0");
    }
    #endregion
}
