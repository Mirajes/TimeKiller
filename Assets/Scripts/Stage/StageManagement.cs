using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageManagement
{
    public A_Stage StarterStage => _starterStage;

    [SerializeField] private Stage_Starter _starterStage;
    [SerializeField] private List<A_Stage> _stages;

    [SerializeField] private int _currentStage = 0;
}
