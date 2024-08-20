using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelPrefabs;
    [SerializeField] private int _currentLevel;

    [SerializeField] private GameObject _levelUI;
    [SerializeField] private GameObject _menuUI;

    private GameObject _scene;

    public int CurrentLevel => _currentLevel;

    private void Start()
    {
        LevelEvents.OnLevelStart += StartLevel;
        LevelEvents.OnLevelWined += WinLevel;
        LevelEvents.OnLevelLose += LoseLevel;
        LevelEvents.OnLevelClose += CloseLevel;
        LevelEvents.OnLevelTryAgain += StartLevel;
        LevelEvents.OnLevelNext += StartLevel;
    }
    private void OnDestroy()
    {
        LevelEvents.OnLevelStart -= StartLevel;
        LevelEvents.OnLevelWined -= WinLevel;
        LevelEvents.OnLevelLose -= LoseLevel;
        LevelEvents.OnLevelClose -= CloseLevel;
        LevelEvents.OnLevelTryAgain -= StartLevel;
        LevelEvents.OnLevelNext -= StartLevel;
    }
    public void StartLevel(int levelIndex)
    {
        _levelUI.SetActive(true);
        _menuUI.SetActive(false);
        ResetScene();
        _currentLevel = levelIndex;
        AssembleScene();

    }
    public void ResetScene()
    {
        if (_scene != null)
        {
            Destroy(_scene);
        }
    }

    private void AssembleScene()
    {
        _scene = Instantiate(_levelPrefabs[_currentLevel]);
        _scene.transform.position = new Vector3(0, 0, 0);
    }

    private void WinLevel()
    {
        ResetScene();
        LevelEvents.DoneLevel(_currentLevel);
    }
    private void LoseLevel()
    {
        ResetScene();
    }
    private void CloseLevel()
    {
        ResetScene();
        _levelUI.SetActive(false);
        _menuUI.SetActive(true);
    }
}
