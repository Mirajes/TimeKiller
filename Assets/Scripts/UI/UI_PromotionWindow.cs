using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PromotionWindow : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private Vector2 _anchorPos;
    [SerializeField] private float _tweenTime = 1f;
    [SerializeField] private Button _closeButton;

    [SerializeField] private TMP_Text _adPointsField;

    [SerializeField] private VerticalLayoutGroup _verticalLayout;
    [SerializeField] private RectTransform _offerContainer;

    [SerializeField] private UI_PromotionOffer _offerTemplate;
    [SerializeField] private List<UI_PromotionOffer> _offerListPrefabs;
    [SerializeField] private int _offerCount = 1;

    private void Start()
    {
        Init();
        UIManager.AdPointChange += OnAdPointChange;
        _closeButton.onClick.AddListener(Close);

        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UIManager.AdPointChange -= OnAdPointChange;
        _closeButton.onClick.RemoveAllListeners();
    }

    public void Init()
    {
        if (_offerCount == 0)
        {
            var noOffer = Instantiate(_offerListPrefabs[0], _offerContainer);
            RecalculateOfferContainer(1);
            return;
        }

        for (int i = 0;  i < _offerContainer.childCount; i++)
        {
            Destroy(_offerContainer.GetChild(i).gameObject);
        }

        for (int i  = 1; i < _offerCount + 1; i++)
        {
            if (i > _offerListPrefabs.Count) continue;
            var newOffer = Instantiate(_offerListPrefabs[i], _offerContainer);
            newOffer.UpdateWindow();
        }

        RecalculateOfferContainer(_offerCount);
    }

    public void Open()
    {
        DOTween.Kill(this.transform);

        this.gameObject.SetActive(false);

        Vector2 startPos = _anchorPos + new Vector2(0, Screen.height * 2);
        _rect.transform.position = startPos;

        this.gameObject.SetActive(true);

        _rect.DOAnchorPos(_anchorPos, _tweenTime);
    }

    public void Close()
    {
        DOTween.Kill(this.transform);

        Vector2 endPos = _anchorPos - new Vector2(0, Screen.height * 2);

        _rect.DOAnchorPos(endPos, _tweenTime).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });

    }

    private void RecalculateOfferContainer(int offerCount)
    {
        float offset = _verticalLayout.padding.vertical;
        float rawSize = _offerTemplate.LayoutElement.preferredHeight * offerCount;
        float expand = _verticalLayout.spacing * (offerCount - 1);

        _offerContainer.sizeDelta = new Vector2(0, offset + rawSize + expand);
    }

    private void OnAdPointChange(int amount)
    {
        _adPointsField.text = "ad points: " + amount.ToString();
    }
    
}
