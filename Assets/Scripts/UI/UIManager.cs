using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// meshanina
public class UIManager : MonoBehaviour
{
    public UI_PauseScreen PauseWindow => _pauseWindow;
    public UI_PromotionWindow PromotionWindow => _promotionWindow;

    [Header("Debug")]
    [SerializeField] private UI_DebugWindow _debugWindow;

    [Header("PauseWindow")]
    [SerializeField] private UI_PauseScreen _pauseWindow;

    [Header("HUD")]
    [SerializeField] private Image _crosshair;
    [SerializeField] private TMP_Text _ammoField;
    [SerializeField] private UI_HealthBar _healthBar;
    [SerializeField] private UI_Timer _timer;

    [Header("Inventory")]
    [SerializeField] private UI_Inventory _inventoryScreen;

    [Header("Notification")] // maybe different place for Sprites will be better?
    [SerializeField] private UI_NotificationPopUp _notificationPrefab;
    [SerializeField] private RectTransform _notificationContainer;
    //[SerializeField] private List<UI_NotificationPopUp> _notificationPopUpList // obj => lazy

    [Header("Promotion")]
    [SerializeField] private UI_PromotionWindow _promotionWindow;

    public static Action<string, NotificationType> Notify;
    public static Action<int> AdPointChange;

    private void OnEnable()
    {
        //_playButton.onClick.AddListener(StartRun);

        GameManager.HealthChange += OnHealthChange;
        //GameManager.TimeEarn += OnTimeEarn;
        //GameManager.TimeSpent += OnTimeUse;

        UIManager.Notify += OnNotify;
    }
    
    private void OnDisable()
    {
        //_playButton.onClick.RemoveAllListeners();

        GameManager.HealthChange -= OnHealthChange;
        //GameManager.TimeEarn -= OnTimeEarn;
        //GameManager.TimeSpent -= OnTimeUse;

        UIManager.Notify -= OnNotify;
    }

    public void Init(Player player)
    {
        _healthBar.ChangeValue(player.CurrentHealth, player.MaxHealth);
        _debugWindow.Init(player);
    }

    private void StartRun()
    {
        GameManager.StartRun?.Invoke();

        //_gameCanvas.gameObject.SetActive(true);
        //_menuCanvas.gameObject.SetActive(false);
        
    }

    private void OnHealthChange(float current, float max)
    {
        _healthBar.ChangeValue(current, max);
    }

    public void SetActiveGameInterface(bool isEnabled)
    {
        _timer.transform.parent.gameObject.SetActive(isEnabled); // :)
        _healthBar.gameObject.SetActive(isEnabled);

        _crosshair.gameObject.SetActive(isEnabled);
    }

    public void OnDebugInput(InputAction.CallbackContext context)
    {
        bool isActive = !_debugWindow.gameObject.activeSelf;
        _debugWindow.gameObject.SetActive(isActive); // why not working by first time if obj is disabled before scene?
    }

    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        bool isActive = !_inventoryScreen.gameObject.activeSelf;
        _inventoryScreen.gameObject.SetActive(isActive);

        if (isActive)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPromoteInput(InputAction.CallbackContext context)
    {
        bool isActive = _promotionWindow.gameObject.activeSelf;
        if (!isActive)
            _promotionWindow.Open();
        else
            _promotionWindow.Close();
    }

    private void OnNotify(string message, NotificationType type)
    {
        var newNotification = Instantiate(_notificationPrefab, _notificationContainer);
        newNotification.transform.SetSiblingIndex(0);

        newNotification.Init(message, type);
    }

    public void BuyItem(SO_Item item)
    {
        _inventoryScreen.DropWindow.AddSlot(item);
    }
}

//private void OnTimeEarn(float time)
//{
//    var newPopUp = Instantiate(_timePopUpPrefab, _timePopUpRect);
//    newPopUp.Init(time, true);
//}

//private void OnTimeUse(float time)
//{
//    var newPopUp = Instantiate(_timePopUpPrefab, _timePopUpRect);
//    newPopUp.Init(time, false);
//}

//[Header("Game")]
//[SerializeField] private Canvas _gameCanvas;
//[SerializeField] private Button _resumeButton;
//[SerializeField] private Button _settingsButton;
//[SerializeField] private Button _exitButton_fromRun;
//[SerializeField] private RectTransform _pauseWindow;
//[SerializeField] private RectTransform _settingsWindow;

//[Header("Menu")]
//[SerializeField] private Canvas _menuCanvas;
//[SerializeField] private Button _playButton;
//[SerializeField] private Button _toCharacterListButton;
//[SerializeField] private Button _settingsButtonMenu;
//[SerializeField] private Button _creditButton;
//[SerializeField] private Button _exitButton_fromGame;
//[SerializeField] private Button _toMenuButton_finishScreen;
//[SerializeField] private RectTransform _menuWindow;
//[SerializeField] private RectTransform _characterWindow;
//[SerializeField] private RectTransform _finishScreen;
