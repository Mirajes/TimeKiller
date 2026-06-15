using UnityEngine;

// soon
public class GameContext : MonoBehaviour
{
    public static GameContext Instance { get; private set; }

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayer()
    {

    }
}

public enum ICharacteristic
{
    Happy,
    Angry,
    Sad,
    Terrified
}