using BepInEx.Configuration;
using MenuLib;
using TMPro;
using UnityEngine;

namespace REPOConfig.Strategies;

internal class BooleanConfigEntryStrategy : ConfigEntryStrategy<bool>
{
    internal BooleanConfigEntryStrategy(ConfigEntryStore configEntryStore) : base(configEntryStore)
    {
    }

    protected override RectTransform Execute(Transform scrollView, string modName, ConfigEntry<bool> entry)
    {
        var repoToggle = MenuAPI.CreateREPOToggle(modName, b =>
        {
            if (ConfigEntryStore.OriginalEntryValues.TryGetValue(entry, out var originalValue) && b == (bool) originalValue)
            {
                ConfigEntryStore.ChangedEntryValues.Remove(entry);
                return;
            }
                                
            ConfigEntryStore.ChangedEntryValues[entry] = b;
        }, scrollView, defaultValue: entry.Value);
        repoToggle.labelTMP.fontStyle = FontStyles.Normal;
        return repoToggle.rectTransform;
    }
}