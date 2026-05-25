using UnityEngine;

[System.Serializable]
public static class GameVariables
{
    [SerializeField] private static readonly float _gravity = -9.81f;

    public static float Gravity => _gravity;
}
