using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UI_TimePopUp : MonoBehaviour
{
    [SerializeField] private float _timeToDissappear = 0.3f;
    [SerializeField] private TMP_Text _timeField;

    public void Init(float timeToText)
    {
        timeToText = -timeToText;

        if (timeToText >= 0)
            _timeField.color = Color.green;
        else
            _timeField.color = Color.red;

        _timeField.text = timeToText.ToString("F2");
        CountToDissappearTask().Forget();
    }

    private async UniTask CountToDissappearTask()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(_timeToDissappear));
        Destroy(this.gameObject);
    }
}