using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DebugWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] private Button _close;

    [SerializeField] private RectTransform _rectTransform;
    //private Vector2 _defaultPosition;

    public void OnDrag(PointerEventData eventData)
    {
        //_rectTransform.pivot = eventData.position; // how?
        _rectTransform.position = eventData.position;
    }

    //private void Start()
    //{
        //_defaultPosition = _rectTransform.anchoredPosition;
    //}

    private void OnDisable()
    {
        _rectTransform.anchoredPosition = new Vector2(-Screen.width / 2, -Screen.height / 2);
    }
}