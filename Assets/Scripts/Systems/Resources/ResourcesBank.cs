using Assets.Scripts.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Systems.Resources
{
    public class ResourcesBank : MonoBehaviour
    {
       // [SerializeField] private ApiManager _apiManager;
        public static ResourcesBank Instance { get; private set; }

        private Dictionary<ResourceTypes, int> _resources = new Dictionary<ResourceTypes, int>();

        public void Initialize()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            InitResourceDictionary();
        }

        public void ModifyResource(ResourceTypes resource, int amount, bool isBetWin = false)
        {
            Debug.Log($"_resources  { _resources[resource]}");
            Debug.Log($"amount  {amount}");
            _resources[resource] += amount;

            SaveManager.SaveResource(resource, _resources[resource]);

            //_apiManager.ChangeScore(SaveManager.LoadResource(ResourceTypes.Coins));
            ResourceEvents.ModifyResource(resource, _resources[resource]);
        }

        public int GetResource(ResourceTypes resource)
        {
            return _resources[resource];
        }

        public bool IsEnoughResource(ResourceTypes resource, int price)
        {
            if(_resources[resource] < price)
            {
                return false;
            }

            return true;
        }

        private void InitResourceDictionary()
        {
            _resources[ResourceTypes.Coins] = SaveManager.LoadResource(ResourceTypes.Coins);

        } 


    }
}