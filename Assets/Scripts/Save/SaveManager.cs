using System;

public static class SaveManager
{
    public static Action<SaveData> Save;
    public static Action<SaveData> Load;

    public static void SaveGameData(SaveData data) { }

    public static void LoadGameData(SaveData data) { }
}
