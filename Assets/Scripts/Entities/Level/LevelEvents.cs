using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelEvents
{
    public static event Action<int> OnLevelsDone;
    public static event Action OnLevelWined;
    public static event Action OnLevelLose;

    public static void DoneLevel(int levelIndex)
    {
        OnLevelsDone?.Invoke(levelIndex);
    }
    public static void WinLevel()
    {
        OnLevelWined?.Invoke();
    }
    public static void LoseLevel(int levelIndex)
    {
        OnLevelLose?.Invoke();
    }
}
