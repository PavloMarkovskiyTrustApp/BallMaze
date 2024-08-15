using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelChooseScreen : BaseScreen
{
    [SerializeField] private List<LevelData> _levels;
    [SerializeField] private int _currentLevel = 0;
    [SerializeField] private TMP_Text _currentLevelText;

    [SerializeField] private Button _Play;
    [SerializeField] private Button[] _levelButtons;

    [Serializable]
    public class LevelData
    {
        public bool IsDone;
        public bool IsOpened;
        public bool IsPressed;
        public GameObject IsDoneSign;
        public Image LevelImage;
        public Sprite LevelDefault;
        public Sprite LevelPressed;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        Subscribe();
    }
    public override void Show()
    {
        base.Show();
        SetLevel();
    }

    private void Subscribe()
    {
        for(int i = 0; i < _levelButtons.Length; i++)
        {
            int index = i;
            _levelButtons[index].onClick.AddListener(() => PressLevelButton(index));
        }
    }
    private void Unsubscribe()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            int index = i;
            _levelButtons[index].onClick.RemoveListener(() => PressLevelButton(index));
        }
    }
    public void SetLevel()
    {
        _currentLevel = 0;
        _levels[_currentLevel].IsPressed = true;
        _currentLevelText.text = $"Level {_currentLevel+1}";
        AcembleLevelButtons();
    }
    
    private void PressLevelButton(int index)
    {
        
        if (_levels[index].IsOpened)
        {
            _levels[_currentLevel].IsPressed = false;
            _currentLevel = index;

            _currentLevelText.text = $"Level {_currentLevel + 1}";

            _levels[_currentLevel].IsPressed = true;

            AcembleLevelButtons();
        }  
    }

    private void AcembleLevelButtons()
    {
        foreach (LevelData level in _levels)
        {

            if (level.IsOpened)
            {
                level.LevelImage.sprite = level.LevelDefault;
            }
            if (level.IsDone)
            {
                level.IsDoneSign.SetActive(true);
            }
            if (level.IsPressed)
            {
                level.LevelImage.sprite = level.LevelPressed;
            }
        }

    }
}
