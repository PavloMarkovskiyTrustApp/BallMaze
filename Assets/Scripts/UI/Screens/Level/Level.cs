using UnityEngine;
using UnityEngine.UI;

public class Level : BaseScreen
{
    [SerializeField] private Button _pauseButton;

    [SerializeField] private BasePopups _winPopup;
    [SerializeField] private BasePopups _losePopup;

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
    }

    private void UnSubscribe()
    {
        LevelEvents.OnLevelWined -= WinLevel;
        LevelEvents.OnLevelLose -= LoseLevel;
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
}
