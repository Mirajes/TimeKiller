using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PromotionWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _adPointsField;

    [SerializeField] private VerticalLayoutGroup _verticalLayout;
    [SerializeField] private RectTransform _offerContainer;

    [SerializeField] private UI_PromotionOffer _offerTemplate;
    [SerializeField] private List<UI_PromotionOffer> _offerListPrefabs;
    [SerializeField] private int _offerCount = 1;

    private void Start()
    {
        RecalculateOfferContainer(_offerCount);
    }

    public void Init()
    {
        if (_offerCount == 0)
        {
            var noOffer = Instantiate(_offerListPrefabs[0], _offerContainer);
            return;
        }

        for (int i  = 0; i < _offerCount; i++)
        {

        }
    }

    private void RecalculateOfferContainer(int offerCount)
    {
        float offset = _verticalLayout.padding.vertical;
        float rawSize = _offerTemplate.LayoutElement.preferredHeight * offerCount;
        float expand = _verticalLayout.spacing * (offerCount - 1);

        _offerContainer.sizeDelta = new Vector2(0, offset + rawSize + expand);
    }
}
