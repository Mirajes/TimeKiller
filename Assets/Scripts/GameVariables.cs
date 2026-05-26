using UnityEngine;

[System.Serializable]
public class GameVariables
{
    public static float Gravity => _gravity;
    [SerializeField] private static float _gravity = -9.81f;
}
