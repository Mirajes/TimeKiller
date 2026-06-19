using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_PauseScreen : MonoBehaviour
{
    [SerializeField] private Button _resumeGame;
    [SerializeField] private Button _openSettings;
    [SerializeField] private Button _exitRun;

    [SerializeField] private UI_TVController _tvController;
    [SerializeField] private UI_Settings _settings;

    private void OnEnable()
    {
        _resumeGame.onClick.AddListener(Resume);
        _openSettings.onClick.AddListener(OpenSettings);
        _exitRun.onClick.AddListener(ExitRun);
    }

    private void OnDisable()
    {
        _resumeGame.onClick.RemoveAllListeners();
        _openSettings.onClick.RemoveAllListeners();
        _exitRun.onClick.RemoveAllListeners();
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        _settings.gameObject.SetActive(false);
        _tvController.Open();

        bool isActive = !this.gameObject.activeSelf;
        this.gameObject.SetActive(isActive);

        AudioManager.Instance.PlaySFX("click");
        GameManager.Pause?.Invoke(isActive);
    }

    private void Resume()
    {
        AudioManager.Instance.PlaySFX("click");
        this.gameObject.SetActive(false);
        GameManager.Pause?.Invoke(false);
    }

    private void OpenSettings()
    {
        _settings.SetTVOpener(_tvController);
        _settings.Open();
        _tvController.Close();

        AudioManager.Instance.PlaySFX("click");
    }

    private void ExitRun()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Menu");

        AudioManager.Instance.PlaySFX("click");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log($"[PauseScreen] - exiting from Run");
    }
}