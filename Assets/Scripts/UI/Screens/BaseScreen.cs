using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseScreen : MonoBehaviour
{
    [SerializeField] protected Button _backButton;
    [SerializeField] protected GameObject _screenView;

    protected UIManager _uiManager;

    public virtual void Initialize(UIManager uiManager)
    {
        _uiManager = uiManager;

        if (_backButton != null)
        {
            _backButton.onClick.RemoveAllListeners();

            _backButton.onClick.AddListener(Hide);
        }
    }

    public GameObject ScreenView => _screenView;

    public virtual void Show()
    {
        _screenView.SetActive(true);
        _uiManager.AddInStack(this);
    }

    public virtual void Hide() => _uiManager.ShowPrevWindow(this);
}
