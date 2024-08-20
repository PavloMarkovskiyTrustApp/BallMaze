using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Put all screens that should be opened first")]
    [SerializeField] private List<BaseScreen> _windowsStack;

    [Header("All windows on Canvas")]
    [SerializeField] private List<BaseScreen> _windows;


    public void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        InitScreens(); 
    }

    public void ShowPrevWindow(BaseScreen currentWindow)
    {
        for (int i = 0; i < _windowsStack.Count; i++)
        {
            Debug.Log(currentWindow);
            Debug.Log(_windowsStack[i]);
            if (currentWindow == _windowsStack[i])
            {

                _windowsStack[i].ScreenView.SetActive(false);
                _windowsStack[i - 1].Show();
                _windowsStack.Remove(_windowsStack[i]);
            }
        }
    } 

    public void ShowScreen(BaseScreen screen)
    {
        foreach(BaseScreen baseScreen in _windows)
        {
            if(screen.GetType() == baseScreen.GetType())
            {
               
                baseScreen.Show();
            }
        }
    }
    public void AddInStack(BaseScreen currentWindow)
    {
        for(int i = 0; i < _windowsStack.Count;i++)
        {
            if(currentWindow == _windowsStack[i])
            {
                return;
            }
        }
        _windowsStack.Add(currentWindow);
    }

    public void RemoveFromStack(BaseScreen currentWindow)
    {
        for (int i = 0; i < _windowsStack.Count; i++)
        {
            if (currentWindow == _windowsStack[i])
            {
                _windowsStack.Remove(currentWindow);
            }
        }
        
    }
    
    private void InitScreens()
    {
        for (int i = 0; i < _windows.Count; i++)
        {
            _windows[i].Initialize(this);
        }
    }

}