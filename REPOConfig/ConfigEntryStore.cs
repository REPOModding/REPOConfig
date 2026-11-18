using System.Collections.Generic;
using BepInEx.Configuration;

namespace REPOConfig
{
    internal class ConfigEntryStore
    {
        internal Dictionary<ConfigEntryBase, object> ChangedEntryValues { get; } = [];
        internal Dictionary<ConfigEntryBase, object> OriginalEntryValues { get; } = [];
    }
}