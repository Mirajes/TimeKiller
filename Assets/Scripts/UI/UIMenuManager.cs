using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    [Header("CharacterEditMenu")]
    [SerializeField] private Button _toCharList;
    [SerializeField] private Button _fromCharList;
    [SerializeField] private RectTransform _redactCharacterMenu;

    [Header("SettingsMenu")]
    [SerializeField] private Button _toSettings;
    [SerializeField] private Button _fromSettings;
    [SerializeField] private UISettings _settingsWindow;

    [Header("Other")]
    [SerializeField] private Button _exitButton;
    [SerializeField] private RectTransform _starterWindow;

    private void Awake()
    {
        _toCharList.onClick.AddListener(SceneLoader);
    }

    private void OnDestroy()
    {
        _toCharList.onClick.RemoveAllListeners();
    }

    private void SceneLoader()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Game");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("[UIMenu] -- loaded");
    }

}
