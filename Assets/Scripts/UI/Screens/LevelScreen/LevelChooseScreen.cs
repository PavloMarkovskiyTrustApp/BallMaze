using Assets.Scripts.Systems.Events;
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
    [Header("Text")]
    [SerializeField] private TMP_Text _coins;

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
        public Sprite LevelClosed;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        Subscribe();
        StartResourceSet();
    }
    public override void Show()
    {
        base.Show();
        ResetScreen();
        SetLevel();
    }

    private void ResetScreen()
    {
        _currentLevel = 0;
        foreach (LevelData level in _levels)
        {
            level.LevelImage.sprite = level.LevelClosed;
            level.IsPressed = false;            
        }
    }
    private void Subscribe()
    {
        ResourceEvents.OnResourceModified += ModifyResource;
        LevelEvents.OnLevelsDone += DoneLevel;
        for(int i = 0; i < _levelButtons.Length; i++)
        {
            int index = i;
            _levelButtons[index].onClick.AddListener(() => PressLevelButton(index));
        }

        _Play.onClick.AddListener(StartLevel);
    }
    private void Unsubscribe()
    {
        ResourceEvents.OnResourceModified -= ModifyResource;
        LevelEvents.OnLevelsDone -= DoneLevel;
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            int index = i;
            _levelButtons[index].onClick.RemoveListener(() => PressLevelButton(index));
        }
        _Play.onClick.RemoveListener(StartLevel);
    }
    private void StartLevel()
    {
        Hide();
        LevelEvents.StartLevel(_currentLevel);
    }
    public void SetLevel()
    {
       
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
    private void DoneLevel(int levelIndex)
    {
        _levels[levelIndex].IsDone = true;
        _levels[levelIndex+1].IsOpened = true;
    }
    private void ModifyResource(ResourceTypes resource, int amount)
    {
        switch (resource)
        {
            case ResourceTypes.Coins: _coins.text = amount.ToString(); break;
        }
    }

    private void StartResourceSet()
    {
        _coins.text = SaveManager.LoadResource(ResourceTypes.Coins).ToString();
    }
}
