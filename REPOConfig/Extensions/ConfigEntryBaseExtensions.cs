using System.Linq;
using BepInEx.Configuration;

namespace REPOConfig.Extensions
{
    internal static class ConfigEntryBaseExtensions
    {
        internal const string HIDE_TAG = "HideFromREPOConfig",
            LEGACY_HIDE_TAG = "HideREPOConfig",
            STEP_TAG = "REPOConfigStep";

        internal static bool HasTag(this ConfigEntryBase entry, string tag) => entry.Description?.Tags?.Contains(tag) == true;

        internal static bool TryGetTagValue(this ConfigEntryBase entry, string tag, out string value)
        {
            var prefix = tag + "=";
            value = entry.Description?.Tags?.OfType<string>().FirstOrDefault(t => t.StartsWith(prefix))?[prefix.Length..].Trim();

            return !string.IsNullOrEmpty(value);
        }
    }
}