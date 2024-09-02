using Assets.Scripts.Systems.Events;
using Assets.Scripts.Systems.Resources;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Home : BaseScreen
{
    [Header("Text")]
    [SerializeField] private TMP_Text _coins;
    [Header("Buttons")]
    [SerializeField] private Button _profile;
    [SerializeField] private Button _info;
    [SerializeField] private Button _leaders;
    [SerializeField] private Button _bonus;
    [SerializeField] private Button _Play;
    [SerializeField] private TextManager textManager;

    private void OnDestroy()
    {
        Unsubscribe();
    }
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        Subscribe();
        textManager.StartResourceSet(_coins);
    }
    public override void Show()
    {
        base.Show();
        textManager.StartResourceSet(_coins);
    }
    private void Subscribe()
    {
        ResourceEvents.OnResourceModified += ModifyResource;

        _profile.onClick.AddListener(() => _uiManager.ShowScreen(new Profile()));
        _info.onClick.AddListener(() => _uiManager.ShowScreen(new Info()));
        _leaders.onClick.AddListener(() => _uiManager.ShowScreen(new LeaderBoard()));
        //_bonus.onClick.AddListener(() => _uiManager.ShowScreen(new Bonus()));
        _Play.onClick.AddListener(() => _uiManager.ShowScreen(new LevelChooseScreen()));

    }
    private void Unsubscribe()
    {
        ResourceEvents.OnResourceModified -= ModifyResource;

        _profile.onClick.RemoveListener(() => _uiManager.ShowScreen(new Profile()));
        _info.onClick.RemoveListener(() => _uiManager.ShowScreen(new Info()));
        _leaders.onClick.RemoveListener(() => _uiManager.ShowScreen(new LeaderBoard()));
        //_bonus.onClick.RemoveListener(() => _uiManager.ShowScreen(new Bonus()));
        _Play.onClick.RemoveListener(() => _uiManager.ShowScreen(new LevelChooseScreen()));

    }

    private void ModifyResource(ResourceTypes resource, int amount)
    {
        textManager.ModifyResource(_coins, amount);
    }

}
