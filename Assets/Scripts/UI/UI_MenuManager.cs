using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MenuManager : MonoBehaviour
{
    [Header("CharacterEditMenu")]
    [SerializeField] private Button _toCharacterList;
    [SerializeField] private Button _fromCharacterList;
    [SerializeField] private RectTransform _redactCharacterMenu;

    [Header("SettingsMenu")]
    [SerializeField] private Button _toSettings;
    [SerializeField] private UI_Settings _settings;

    [Header("Other")]
    [SerializeField] private Button _exitButton;
    [SerializeField] private RectTransform _starterWindow;

    private void Awake()
    {
        _settings.Init();

        _toCharacterList.onClick.AddListener(() => SceneLoader("Game"));
        _exitButton.onClick.AddListener(() => SceneLoader("Menu"));

        _toSettings.onClick.AddListener(_settings.Open);
    }

    private void OnDestroy()
    {
        _toCharacterList.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();

        _toSettings.onClick.RemoveAllListeners();
    }

    private void SceneLoader(string sceneName)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("[UIMenu] -- loaded");
    }

}