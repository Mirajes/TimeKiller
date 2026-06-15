using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Timer
{
    [SerializeField] private float _starterTime;
    [SerializeField] private float _currentTime;

    private async UniTask CountdownTask() 
    {
        await UniTask.Yield();

        _currentTime -= Time.deltaTime;
    }
}
