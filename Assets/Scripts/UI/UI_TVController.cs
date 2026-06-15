using DG.Tweening;
using UnityEngine;

public class UI_TVController : MonoBehaviour
{
    [Header("DOTween")]
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _tweenSpeed;
    private Vector2 _anchorPosition;

    private void OnDestroy()
    {
        DOTween.Kill(_rectTransform);
    }

    public void Init()
    {
        _anchorPosition = _rectTransform.anchoredPosition;
        Open();
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

        _rectTransform.DOAnchorPos(_anchorPosition, _tweenSpeed);
    }

    public void Close()
    {
        DOTween.Kill(_rectTransform);

        float rectHeight = _rectTransform.rect.height;

        Vector2 endPosition = new Vector2(
            _anchorPosition.x,
            -rectHeight
            );

        _rectTransform.DOAnchorPos(endPosition, _tweenSpeed).OnComplete(() =>
        {
            _rectTransform.gameObject.SetActive(false);
        });
    }
}