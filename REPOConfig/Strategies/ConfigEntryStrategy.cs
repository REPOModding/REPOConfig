using System;
using BepInEx.Configuration;
using UnityEngine;

namespace REPOConfig.Strategies;

internal abstract class ConfigEntryStrategy<T> : IConfigEntryStrategy
{
    public Type TargetType { get; } = typeof(T);
    protected ConfigEntryStore ConfigEntryStore { get; }

    protected ConfigEntryStrategy(ConfigEntryStore configEntryStore)
    {
        ConfigEntryStore = configEntryStore;
    }
        
    public RectTransform Execute(Transform scrollView, string modName, ConfigEntryBase entry)
    {
        return Execute(scrollView, modName, (ConfigEntry<T>)entry);
    }
        
    protected abstract RectTransform Execute(Transform scrollView, string modName, ConfigEntry<T> entry);
}
    
internal interface IConfigEntryStrategy
{
    Type TargetType { get; }

    RectTransform Execute(Transform scrollView, string modName, ConfigEntryBase entry);
}