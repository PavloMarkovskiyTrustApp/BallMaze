using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Popups
{
    public class LosePopup : BasePopups
    {
        [SerializeField] private Button _tryAgain;
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
            _tryAgain.onClick.AddListener(TryAgain);
            _mainMenu.onClick.AddListener(MainMenu);
        }

        private void UnSubscribe()
        {
            _tryAgain.onClick.RemoveListener(TryAgain);
            _mainMenu.onClick.RemoveListener(MainMenu);
        }

        private void TryAgain()
        {
            LevelEvents.TryAgain(_levelManager.CurrentLevel);
            Hide();
        }

        private void MainMenu()
        {
            LevelEvents.CloseLevel();
            Hide();
        }
    }
}