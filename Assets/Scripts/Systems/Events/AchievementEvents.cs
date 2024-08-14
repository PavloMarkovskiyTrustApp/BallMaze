using Assets.Scripts.Game.Achievements;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Systems.Events
{
    public static class AchievementEvents
    {

        public static event Action<AchievementTypes> OnAchieved;
        public static event Action OnNewAchieve;

        public static void Achieve(AchievementTypes achievementTypes)
        {
            OnAchieved?.Invoke(achievementTypes);
        }
        public static void GotNewAchieve()
        {
            OnNewAchieve?.Invoke();
        }
    }
}