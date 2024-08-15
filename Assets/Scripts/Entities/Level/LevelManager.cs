using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelPrefabs;
    [SerializeField] private int _currentLevel;

    private GameObject _scene;

    private void Start()
    {
        LevelEvents.OnLevelWined += WinLevel;
        LevelEvents.OnLevelLose += LoseLevel;
    }
    private void OnDestroy()
    {
        LevelEvents.OnLevelWined -= WinLevel;
        LevelEvents.OnLevelLose -= LoseLevel;
    }
    public void StartLevel(int levelIndex)
    {
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
        _scene = Instantiate(_levelPrefabs[0]);
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
}
