using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private bool _isActive = false;

    public void ChangeValue(float current, float max)
    {
        float value = current / max;
    }
}