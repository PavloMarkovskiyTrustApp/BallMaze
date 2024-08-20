using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePopups : MonoBehaviour
{
    [SerializeField] protected Button _mainButton;
    [SerializeField] protected GameObject _view;

    public void Start()
    {
        Init();
       
    }
    public virtual void Init()
    {
        _mainButton.onClick.AddListener(MainButtonPressed);
    }
    public virtual void Show() => _view.SetActive(true);

    public virtual void Hide() => _view.SetActive(false);

    public abstract void MainButtonPressed();
}
