
using Assets.Scripts.Systems.Events;
using Assets.Scripts.Systems.Resources;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{

    [SerializeField] private UIManager uiManager;
    [SerializeField] private ResourcesBank _bank;
    [SerializeField] private bool _devMode;

    private void Awake()
    {
        if (_devMode)
        {
            SaveManager.DeleteAllKey();
        }
        
    }

    private void Start()
    {
        _bank.Initialize();

        uiManager.Initialize();
        
    }
}
