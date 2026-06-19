using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource; // ?
    [SerializeField] private AudioSource _voiceSource; // ?

    [SerializedDictionary("SFX Name", "Clip")] [SerializeField]
    SerializedDictionary<string, AudioClip> _sfxList = new();
    [SerializeField] private AudioClip _placeholder;

    private const string GENERAL_VOLUME_KEY = "GeneralVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string VOICE_VOLUME_KEY = "VoiceVolume";

    private float _generalVolume = 1.0f;
    private float _musicVolume = 1.0f;
    private float _sfxVolume = 1.0f;
    private float _voiceVolume = 1.0f;
    
    public float GeneralVolume => _generalVolume;
    public float MusicVolume => _musicVolume;
    public float SFXVolume => _sfxVolume;
    public float VoiceVolume => _voiceVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            LoadVolumeSettings();
            ApplyAllVolumes();
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

    }

    private void LoadVolumeSettings()
    {
        _generalVolume = PlayerPrefs.GetFloat(GENERAL_VOLUME_KEY, 1f);
        _musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
        _sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
        _voiceVolume = PlayerPrefs.GetFloat(VOICE_VOLUME_KEY, 1f);
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(GENERAL_VOLUME_KEY, _generalVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY , _musicVolume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, _sfxVolume);
        PlayerPrefs.SetFloat(VOICE_VOLUME_KEY, _voiceVolume);

        PlayerPrefs.Save();
    }

    private void ApplyAllVolumes()
    {
        ApplyVolume(_musicSource, _musicVolume * _generalVolume);
        ApplyVolume(_sfxSource, _sfxVolume * _generalVolume);
        ApplyVolume(_voiceSource, _voiceVolume * _generalVolume);
    }

    private void ApplyVolume(AudioSource source, float volume)
    {
        source.volume = volume;
    }
    
    public void OnGeneralVolumeChange(float value) 
    {
        _generalVolume = value;
        ApplyAllVolumes();
        SaveVolumeSettings();
    }

    public void OnMusicVolumeChange(float value)
    {
        _musicVolume = value;
        ApplyVolume(_musicSource, _musicVolume * _generalVolume);
        SaveVolumeSettings();
    }

    public void OnSFXVolumeChange(float value)
    {
        _sfxVolume = value;
        ApplyVolume(_sfxSource, _sfxVolume * _generalVolume);
        SaveVolumeSettings();
    }

    public void OnVoiceVolumeChange(float value)
    {
        _voiceVolume = value;
        ApplyVolume(_voiceSource, _voiceVolume * _generalVolume);
        SaveVolumeSettings();
    }

    public void PlayPositionedSound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        // ??
    }

    private AudioClip GetSoundByName(string name)
    {
        if (_sfxList.TryGetValue(name, out AudioClip clip))
        {
            return clip;
        }
        else
        {
            return _placeholder;
        }
    }

    public void PlaySFX(string soundName)
    {
        _sfxSource.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
        _sfxSource.PlayOneShot(GetSoundByName(soundName));
    }

    public void PlayMusic(string musicName)
    {
        _musicSource.Stop();
        _musicSource.clip = GetSoundByName(musicName);
        _musicSource.Play();
    }
}
