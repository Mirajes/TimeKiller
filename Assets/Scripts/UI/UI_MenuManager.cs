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
    [SerializeField] private Button _fromSettings;
    [SerializeField] private UI_Settings _settingsWindow;

    [Header("Other")]
    [SerializeField] private Button _exitButton;
    [SerializeField] private RectTransform _starterWindow;

    private void Awake()
    {
        _toCharacterList.onClick.AddListener(ToGameScene);
        _exitButton.onClick.AddListener(ToMenuScene);
    }

    private void OnDestroy()
    {
        _toCharacterList.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    private void ToGameScene()
    {
        SceneLoader("Game");
    }

    private void ToMenuScene()
    {
        SceneLoader("Menu");
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