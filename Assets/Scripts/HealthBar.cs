using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _field;
    //[SerializeField] private int _maxSections = 5;
    //[SerializeField] private string _sectionSymbol = "O";
    [SerializeField] private float _timeToDisappear = 5f;

    CancellationTokenSource _cts;

    private void Update()
    {
        _field.transform.LookAt(-Camera.main.transform.position);
    }
    public void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    // now in works on strings
    public void ChangeValue(float current, float max)
    {
        float value = current / max;

        //int sectionCount = (int)Math.Ceiling(value * _maxSections);
        //_field.text = string.Join(_sectionSymbol, sectionCount);
        _field.text = current.ToString();

        _cts?.Cancel();
        _cts?.Dispose();

        _cts = new();
        ShowHealthBarTask(_cts.Token).Forget();
    }

    private async UniTask ShowHealthBarTask(CancellationToken token)
    {
        this.gameObject.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDisappear), cancellationToken: token);

        this.gameObject.SetActive(false);
    }

}