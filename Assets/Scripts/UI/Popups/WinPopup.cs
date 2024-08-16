using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Popups
{
    public class WinPopup : BasePopups
    {
        [SerializeField] private Button _nextLevel;
        [SerializeField] private Button _mainMenu;
        [SerializeField] private LevelManager _levelManager;

        public override void MainButtonPressed()
        {

        }

        public override void Init()
        {
            base.Init();
            Subscribe();
        }
        public void OnDestroy()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            _nextLevel.onClick.AddListener(NextLevel);
            _mainMenu.onClick.AddListener(MainMenu);
        }

        private void UnSubscribe()
        {
            _nextLevel.onClick.RemoveListener(NextLevel);
            _mainMenu.onClick.RemoveListener(MainMenu);
        }

        private void NextLevel()
        {
            LevelEvents.NextLevel(_levelManager.CurrentLevel+1);
            Hide();
        }

        private void MainMenu()
        {
            LevelEvents.CloseLevel();
            Hide();
        }

    }
}