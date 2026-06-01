using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthImage;
    [SerializeField] private TMP_Text _healthText;

    [SerializeField] private Outline _outline;

    public void ChangeValue(float current, float max)
    {
        float value = current / max;
        
        _healthImage.fillAmount = value;
        _healthText.text = current.ToString("F2");
    }
}