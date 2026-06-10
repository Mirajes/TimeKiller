using TMPro;
using UnityEngine;

public class UI_Tip : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private RectTransform _rectTransform;

    public RectTransform RectTransform => _rectTransform;

    public void UpdateTip(string text = "", bool isActive = false)
    {
        if (gameObject == null) return;

        if (isActive)
        {
            _textField.text = text;
            this.gameObject.SetActive(true);
        }
        else
        {
            _textField.text = string.Empty;
            this.gameObject.SetActive(false);
        }
    }
}