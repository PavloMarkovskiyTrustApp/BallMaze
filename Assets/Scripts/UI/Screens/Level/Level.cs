using UnityEngine;
using UnityEngine.UI;

public class Level : BaseScreen
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _mainMenuButton;

    [SerializeField] private BasePopups _winPopup;
    [SerializeField] private BasePopups _losePopup;
    [SerializeField] private BasePopups _pausePopup;

    [SerializeField] private LevelManager _levelManager;


    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        Subscribe();
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }

    private void Subscribe()
    {
        LevelEvents.OnLevelWined += WinLevel;
        LevelEvents.OnLevelLose += LoseLevel;
        LevelEvents.OnPause += PauseActivities;

        _mainMenuButton.onClick.AddListener(MainMenu);
        _pauseButton.onClick.AddListener(Pause);
    }

    private void UnSubscribe()
    {
        LevelEvents.OnLevelWined -= WinLevel;
        LevelEvents.OnLevelLose -= LoseLevel;
        LevelEvents.OnPause -= PauseActivities;

        _mainMenuButton.onClick.RemoveListener(MainMenu);
        _pauseButton.onClick.RemoveListener(Pause);
    }

    private void LoseLevel()
    {
        Debug.Log("Show" + _losePopup);
        _losePopup.Show();
    }

    private void WinLevel()
    {
        _winPopup.Show();
    }

    private void Pause()
    {
        LevelEvents.Pause(true);
        
    }

    private void PauseActivities(bool isPaused)
    {
        if(isPaused)
        {
            _pausePopup.Show();
        }
    }
    private void MainMenu()
    {
        LevelEvents.CloseLevel();
    }
}
