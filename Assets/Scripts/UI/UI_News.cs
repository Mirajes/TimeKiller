using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

// ultrawide monitor breaking me
public class UI_News : MonoBehaviour
{
    StringBuilder _stringBuilder = new(256); // > 64 needed?

    [SerializeField] private TMP_Text _newsField;
    [SerializeField] private float _charPerSec = 10f;
    [SerializeField] private int _charCountInRow = 55;

    [SerializeField] private string _spaceChar = "\u00A0"; // space (but issue was in TMP_Text => wrap mode
    [SerializeField] private int _spaceCount = 20;

    [SerializeField] private List<string> _newsList = new();


    private async UniTask WritterTask()
    {
        _stringBuilder.Clear();
        _newsField.text = string.Empty;

        if (_newsList.Count == 0) {Debug.LogWarning("[News] -- no news"); return; }

        while (true)
        {
            foreach (string topic in _newsList)
            {

                foreach (char c in topic)
                {
                    await UniTask.Delay(System.TimeSpan.FromSeconds(1 / _charPerSec));

                    if (_stringBuilder.Length >= _charCountInRow)
                    {
                        MoveForward(c.ToString());
                    }
                    else
                    {
                        _stringBuilder.Append(c);
                    }

                    _newsField.text = _stringBuilder.ToString();
                }

                for (int i = 0; i < _spaceCount; i++)
                {
                    await UniTask.Delay(System.TimeSpan.FromSeconds(1 / _charPerSec));

                    if (_stringBuilder.Length >= _charCountInRow)
                        MoveForward(_spaceChar);
                    else 
                        _stringBuilder.Append(_spaceChar);
                    
                    _newsField.text = _stringBuilder.ToString();
                }
            }
        } 
    }

    private void MoveForward(string newChar)
    {
        _stringBuilder.Remove(0, 1);
        _stringBuilder.Append(newChar);
    }

    private void Start()
    {
        WritterTask().Forget();
    }
}
