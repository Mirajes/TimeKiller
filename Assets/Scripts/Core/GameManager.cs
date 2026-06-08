using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private UIManager _uiManager;

    [SerializeField] private StageManagement _stageManagement = new();
    private InputHandler _inputHandler = new();

    // remake
    public static Action<float, float> HealthChange;
    public static Action<float> TimeEarn;
    public static Action<float> TimeUse;
    public static Action StartRun;
    //
}

[System.Serializable]
public class StageManagement
{
    [SerializeField] private Stage_Starter _starterStage;
    [SerializeField] private List<A_Stage> _stages;

    [SerializeField] private int _currentStage = 0;
}

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}

public class StateMachine
{

}

[System.Serializable]
public class Timer
{
    [SerializeField] private float _starterTime;
    [SerializeField] private float _currentTime;

    private async UniTask CountdownTask() { }
}



public static class SaveManager
{
    public static Action<SaveData> Save;
    public static Action<SaveData> Load;

    //public static void SaveGame(SaveData data) { }

    //public static void LoadGame(SaveData data) { }
}

[System.Serializable]
public class SaveData
{
    public SerializedDictionary<string, string> Data = new();
}

public interface ISaveable
{
    void OnSave(SaveData data);
    void OnLoad(SaveData data);
}

public class AudioManager : MonoBehaviour
{

}
