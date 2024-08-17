using Assets.Scripts.Systems.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Achievements
{
    public class AchievementManager : MonoBehaviour
    {
        public static AchievementManager instance;

        [SerializeField]private List<AchievementTypes> _achieved = new List<AchievementTypes>();

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            Subscribe();
            
            if(SaveManager.IsSaved(SaveKeys.Achievements))
            {
                _achieved = SaveManager.LoadAchievementList(SaveKeys.Achievements);
            }
            
        }
        public void OnDestroy()
        {
            Unsubscribe();
        }
        private void Subscribe()
        {
            AchievementEvents.OnAchieved += Achieve;
        }
        public void Unsubscribe()
        {
            AchievementEvents.OnAchieved -= Achieve;
        }
        public void Achieve(AchievementTypes achievementType)
        {
            Debug.Log("achieved: " + achievementType);
            foreach(AchievementTypes achievement in _achieved)
            {
                if(achievement == achievementType)
                {
                    return; 
                }
            }
            _achieved.Add(achievementType);
            Debug.Log($"Achievement receved: { _achieved[_achieved.Count -1] }");
            SaveManager.SaveAchievementList(SaveKeys.Achievements, _achieved);
            AchievementEvents.GotNewAchieve();
        }
    }
}