using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelEvents
{
    public static event Action<int> OnLevelsDone;
    public static event Action OnLevelWined;
    public static event Action OnLevelLose;
    public static event Action<int> OnLevelStart;
    public static event Action OnLevelClose;
    public static event Action<int> OnLevelTryAgain;
    public static event Action<int> OnLevelNext;
    public static event Action<bool> OnPause;
    public static void DoneLevel(int levelIndex)
    {
        OnLevelsDone?.Invoke(levelIndex);
    }
    public static void WinLevel()
    {
        OnLevelWined?.Invoke();
    }
    public static void LoseLevel()
    {
        OnLevelLose?.Invoke();
    }
    public static void StartLevel(int currentLevel)
    {
        OnLevelStart?.Invoke(currentLevel);
    }
    public static void CloseLevel()
    {
        OnLevelClose?.Invoke();
    }
    public static void TryAgain(int levelIndex)
    {
        OnLevelTryAgain?.Invoke(levelIndex);
    }
    public static void NextLevel(int levelIndex)
    {
        OnLevelNext?.Invoke(levelIndex);
    }
    public static void Pause(bool isPaused)
    {
        OnPause?.Invoke(isPaused);
    }
}
