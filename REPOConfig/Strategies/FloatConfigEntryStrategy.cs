using System;
using BepInEx.Configuration;
using MenuLib;
using REPOConfig.Extensions;
using TMPro;
using UnityEngine;

namespace REPOConfig.Strategies;

internal class FloatConfigEntryStrategy : ConfigEntryStrategy<float>
{
    internal FloatConfigEntryStrategy(ConfigEntryStore configEntryStore) : base(configEntryStore)
    {
    }

    protected override RectTransform Execute(Transform scrollView, string modName, ConfigEntry<float> entry)
    {
        float min, max;
        var precision = 2;

        if (entry.Description.AcceptableValues is AcceptableValueRange<float> acceptableValueRange)
        {
            min = acceptableValueRange.MinValue;
            max = acceptableValueRange.MaxValue;

            precision = Mathf.Max(min.GetDecimalPlaces(), max.GetDecimalPlaces(),
                ((float)entry.DefaultValue).GetDecimalPlaces(), 2);
        }
        else
        {
            var absoluteDefaultValue = Math.Abs(entry.Value);

            if (absoluteDefaultValue == 0)
                min = -(max = 100);
            else if (absoluteDefaultValue <= .001)
                min = -(max = 10f);
            else if (absoluteDefaultValue <= .01)
                min = -(max = 50f);
            else if (absoluteDefaultValue <= 100)
                min = -(max = absoluteDefaultValue * 3f);
            else
                min = -(max = absoluteDefaultValue * 2);
        }

        var repoSlider = MenuAPI.CreateREPOSlider(modName, string.Empty, f => //description
        {
            if (ConfigEntryStore.OriginalEntryValues.TryGetValue(entry, out var originalValue) && Math.Abs(f - (float)originalValue) < float.Epsilon)
            {
                ConfigEntryStore.ChangedEntryValues.Remove(entry);
                return;
            }

            ConfigEntryStore.ChangedEntryValues[entry] = f;
        }, scrollView, defaultValue: entry.Value, min: min, max: max, precision: precision);
        repoSlider.descriptionTMP.fontStyle = repoSlider.labelTMP.fontStyle = FontStyles.Normal;

        /*if (description.Length <= 43)
            return repoSlider.rectTransform;*/

        //repoSlider.descriptionTMP.maxVisibleCharacters = repoSlider.repoTextScroller.maxCharacters = 43;
        //repoSlider.repoTextScroller.scrollingSpeedInSecondsPerCharacter = Entry.descriptionScrollSpeed.Value;

        /*repoSlider.repoTextScroller.endWaitTime = repoSlider.repoTextScroller.initialWaitTime = 5f;
        repoSlider.repoTextScroller.startWaitTime = 3f;

        repoSlider.descriptionTMP.alignment = TextAlignmentOptions.Left;
        modPage.StartCoroutine(repoSlider.repoTextScroller.Animate());*/


        return repoSlider.rectTransform;
    }
}