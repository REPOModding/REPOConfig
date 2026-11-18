using BepInEx.Configuration;
using MenuLib;
using TMPro;
using UnityEngine;

namespace REPOConfig.Strategies;

internal class StringConfigEntryStrategy : ConfigEntryStrategy<string>
{
    internal StringConfigEntryStrategy(ConfigEntryStore configEntryStore) : base(configEntryStore)
    {
    }

    protected override RectTransform Execute(Transform scrollView, string modName, ConfigEntry<string> entry)
    {
        return entry.Description.AcceptableValues is AcceptableValueList<string> acceptableValueList
            ? HandleListEntry(scrollView, modName, entry, acceptableValueList)
            : HandleStringEntry(scrollView, modName, entry);
    }

    private RectTransform HandleStringEntry(Transform scrollView, string modName, ConfigEntry<string> entry)
    {
        var defaultValue = (string) entry.DefaultValue;
                            
        var repoInputField = MenuAPI.CreateREPOInputField(modName, s =>
        {
            if (ConfigEntryStore.OriginalEntryValues.TryGetValue(entry, out var originalValue) && s == (string) originalValue)
            {
                ConfigEntryStore.ChangedEntryValues.Remove(entry);
                return;
            }
                                
            ConfigEntryStore.ChangedEntryValues[entry] = s;
        }, scrollView, Vector2.zero, false, !string.IsNullOrEmpty(defaultValue) ? defaultValue : "<NONE>", entry.Value);
        repoInputField.labelTMP.fontStyle = repoInputField.inputStringSystem.inputTMP.fontStyle = FontStyles.Normal;
                            
        return repoInputField.rectTransform;
    }
        
    private RectTransform HandleListEntry(Transform scrollView, string modName, ConfigEntry<string> entry, AcceptableValueList<string> acceptableValueList)
    {
        var repoSlider = MenuAPI.CreateREPOSlider(modName, string.Empty, s => //description
        {
            if (ConfigEntryStore.OriginalEntryValues.TryGetValue(entry, out var originalValue) && s == (string) originalValue)
            {
                ConfigEntryStore.ChangedEntryValues.Remove(entry);
                return;
            }
                                
            ConfigEntryStore.ChangedEntryValues[entry] = s;
        }, scrollView, acceptableValueList.AcceptableValues, entry.Value);
        repoSlider.descriptionTMP.fontStyle = repoSlider.labelTMP.fontStyle = FontStyles.Normal;
                            
        /*if (description.Length <= 43)
            return repoSlider.rectTransform;

        repoSlider.descriptionTMP.maxVisibleCharacters = repoSlider.repoTextScroller.maxCharacters = 43;
        repoSlider.repoTextScroller.scrollingSpeedInSecondsPerCharacter = Entry.descriptionScrollSpeed.Value;

        repoSlider.repoTextScroller.endWaitTime = repoSlider.repoTextScroller.initialWaitTime = 5f;
        repoSlider.repoTextScroller.startWaitTime = 3f;

        repoSlider.descriptionTMP.alignment = TextAlignmentOptions.Left;
        modPage.StartCoroutine(repoSlider.repoTextScroller.Animate());*/
                            
        return repoSlider.rectTransform;
    }
}