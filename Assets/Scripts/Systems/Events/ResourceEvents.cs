using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Systems.Events
{
    public static class ResourceEvents
    {
        public static event Action<ResourceTypes,int> OnResourceModified;

        public static void ModifyResource(ResourceTypes resource, int currentAmount)
        {
            OnResourceModified?.Invoke(resource, currentAmount);
        }
    }
}