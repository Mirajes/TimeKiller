using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UI_TimePopUp : MonoBehaviour
{
    [SerializeField] private float _timeToDissappear = 0.3f;
    [SerializeField] private TMP_Text _timeField;

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
        CountToDissappearTask().Forget();
    }

    private async UniTask CountToDissappearTask()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(_timeToDissappear));
        Destroy(this.gameObject);
    }
}