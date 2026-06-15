using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UI_TimePopUp : MonoBehaviour
{
    [SerializeField] private float _timeToDisappear = 0.3f;
    [SerializeField] private TMP_Text _timeField;
    [SerializeField] private RectTransform _rect;

    public void Init(float timeToText, bool isGiven)
    {
        string text = string.Empty;
        if (isGiven)
        {
            _timeField.color = Color.green; 
            text += "+"; 
        }
        else
        {
            _timeField.color = Color.red;
            text += "-"; 
        }

        text += timeToText.ToString("F2");
        _timeField.text = text;
        CountToDisappearTask().Forget();
    }

    private async UniTask CountToDisappearTask()
    {
        _rect.DOAnchorPosY(10f, _timeToDisappear);
        await UniTask.Delay(System.TimeSpan.FromSeconds(_timeToDisappear));
        Destroy(this.gameObject);
    }
}