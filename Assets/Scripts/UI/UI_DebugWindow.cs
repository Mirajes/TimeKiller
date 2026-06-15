using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DebugWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] private Button _close;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Button _damagePlayerButton;
    [SerializeField] private Button _healPlayerButton;
    [SerializeField] private Button _notifyButton;

    private Player _player;
    private Vector2 _defaultPosition;

    public void Init(Player player)
    {
        _player = player;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //_rectTransform.pivot = eventData.position; // how?
        _rectTransform.position = eventData.position;
    }

    private void Start()
    {
        this.gameObject.SetActive(true);
        _defaultPosition = _rectTransform.anchoredPosition;
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _damagePlayerButton.onClick.AddListener(DamagePlayer);
        _healPlayerButton.onClick.AddListener(HealPlayer);
        _notifyButton.onClick.AddListener(Notify);
    }

    private void OnDisable()
    {
        _rectTransform.anchoredPosition = new Vector2(-Screen.width / 2, -Screen.height / 2);

        _damagePlayerButton.onClick.RemoveAllListeners();
        _healPlayerButton.onClick.RemoveAllListeners();
        _notifyButton.onClick.RemoveAllListeners();
    }

    private void DamagePlayer()
    {
        _player.HandleHit(10);
    }

    private void HealPlayer()
    {
        _player.HandleHeal(1);
    }

    private void Notify()
    {
        UIManager.Notify?.Invoke("hey, that's a debug window with letter than have more than 40 letters", NotificationType.Alert);
    }
}