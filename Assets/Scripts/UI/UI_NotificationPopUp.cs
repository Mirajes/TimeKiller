using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_NotificationPopUp : MonoBehaviour, IPointerClickHandler
{
    [Header("Core")]
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _notification;
    [SerializeField] private Image _progressBar;
    private float _timeProgress = 0;

    [Header("Settings")]
    [SerializeField] private int _charLimit = 40;
    [SerializeField] private float _lifeTime = 5f;

    [Header("Sprites")]
    [SerializeField] private Sprite _notifySprite;
    [SerializeField] private Sprite _announcementSprite;
    [SerializeField] private Sprite _alertSprite;
    [SerializeField] private Sprite _buySprite;

    CancellationTokenSource _cts;

    public void Init(string text, NotificationType type)
    {
        _icon.sprite = GetNotificationSprite(type);
        _notification.text = text.Substring(0, _charLimit) + "...";

        _cts = new();
        this.transform.localScale = Vector3.zero;
        _timeProgress = 0f;
        _progressBar.fillAmount = 0f;

        this.gameObject.SetActive(true);
        this.transform.DOScale(1.2f, 1f).OnComplete(() =>
        {
            this.transform.DOScale(1f, 0.1f);
        });

        CycleOfLife(_cts.Token).Forget();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _cts?.Cancel();
        _cts?.Dispose();

        Hide();
    }

    private async UniTask CycleOfLife(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await UniTask.Yield(token, true);
            _timeProgress += Time.deltaTime;

            _progressBar.fillAmount = _timeProgress / _lifeTime;

            if (_timeProgress >= _lifeTime)
                break;
        }
        
        Hide();
    }

    private void Hide()
    {
        DOTween.Kill(this.transform);

        this.transform.DOScale(0f, 0.5f).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }

    private Sprite GetNotificationSprite(NotificationType type)
    {
        switch (type)
        {
            case NotificationType.Notify:
                return _notifySprite;
            case NotificationType.Announcement:
                return _announcementSprite;
            case NotificationType.Alert:
                return _alertSprite;
            case NotificationType.Buy:
                return _buySprite;
            default:
                Debug.LogWarning($"[UIManager] - no sprite for {type}");
                return null;
        }
    }
}

