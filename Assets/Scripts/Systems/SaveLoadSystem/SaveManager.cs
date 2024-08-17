using Assets.Scripts.Game.Achievements;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveManager
{

    public static void SaveResource(ResourceTypes resource, int amount)
    {
        switch (resource)
        {
            case ResourceTypes.Coins:
                PlayerPrefs.SetInt(SaveKeys.PlayerPoints, amount);
                PlayerPrefs.Save();
                break;
        }
    }
    public static int LoadResource(ResourceTypes resource)
    {
        switch (resource)
        {     
            case ResourceTypes.Coins:
                    if (IsSaved(SaveKeys.PlayerPoints))
                        return PlayerPrefs.GetInt(SaveKeys.PlayerPoints, 0);
                break;
        }
        return 0;
    }

    // Метод для сохранения int
    public static void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    // Метод для загрузки int
    public static int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }
    // Метод для сохранения float
    public static void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    // Метод для загрузки float
    public static float LoadFloat(string key, float defaultValue = 0.0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }
    // Метод для сохранения string
    public static void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    // Метод для загрузки string
    public static string LoadString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }
    // Метод для сохранения List<int>
    public static void SaveIntList(string key, List<int> list)
    {
        string serializedList = string.Join(",", list);
        PlayerPrefs.SetString(key, serializedList);
        PlayerPrefs.Save();
    }

    // Метод для загрузки List<int>
    public static List<int> LoadIntList(string key)
    {
        string serializedList = PlayerPrefs.GetString(key, string.Empty);
        if (string.IsNullOrEmpty(serializedList))
        {
            return new List<int>();
        }

        return serializedList.Split(',').Select(int.Parse).ToList();
    }
    // Метод для сохранения List<AchievementTypes>
    public static void SaveAchievementList(string key, List<AchievementTypes> list)
    {
        Debug.Log(key + " sdsd");
        string serializedList = string.Join(",", list.Select(a => a.ToString()).ToArray());
        PlayerPrefs.SetString(key, serializedList);
        PlayerPrefs.Save();
        Debug.Log(key);
    }

    // Метод для загрузки List<AchievementTypes>
    public static List<AchievementTypes> LoadAchievementList(string key)
    {
        string serializedList = PlayerPrefs.GetString(key, string.Empty);
        if (string.IsNullOrEmpty(serializedList))
        {
            return new List<AchievementTypes>();
        }

        return serializedList.Split(',')
                             .Select(s => (AchievementTypes)System.Enum.Parse(typeof(AchievementTypes), s))
                             .ToList();
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    // Метод для удаления сохранения
    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public static bool IsSaved(string key)
    {
        if (HasKey(key))
        {
            return true;
        }
        return false;
    }
    public static void DeleteAllKey()
    {

        PlayerPrefs.DeleteKey(SaveKeys.PlayerPoints);

        PlayerPrefs.DeleteKey(SaveKeys.PlayerID);
        PlayerPrefs.DeleteKey(SaveKeys.PlayerName);
        PlayerPrefs.DeleteKey(SaveKeys.FirstRunKey);

        PlayerPrefs.DeleteKey(SaveKeys.Achievements);

        PlayerPrefs.DeleteKey(SaveKeys.PlayerAvatarPath);
        PlayerPrefs.DeleteKey(SaveKeys.DoneLevelList);

        PlayerPrefs.DeleteKey(SaveKeys.TotalCoins);
        PlayerPrefs.DeleteKey(SaveKeys.TotalBonuses);
    }
}