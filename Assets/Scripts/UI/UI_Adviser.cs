using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Adviser : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string _tipText;
    [SerializeField] private float _tipWaitTime = 1f;
    private float _tipTime = 0f;

    [SerializeField] private UI_Tip _tip;
    [SerializeField] private RectTransform _rectTransform;

    CancellationTokenSource _cts;

    private async UniTask CountdownTimeTask()
    {         
        _cts = new();

        while (_cts != null && !_cts.Token.IsCancellationRequested)
        {
            await UniTask.Yield(); // for deltaTime
            _tipTime += Time.deltaTime;
            if (_tipTime >= _tipWaitTime)
            {
                if (_tip == null) return;

                _tip.UpdateTip(_tipText, true);
                break;
            }
        }

        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CountdownTimeTask().Forget();

        _tip.transform.position = eventData.position;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        _tip.UpdateTip();
        _tipTime = 0;
    }
}
