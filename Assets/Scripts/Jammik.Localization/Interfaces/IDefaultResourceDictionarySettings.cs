using UnityEngine;

namespace Jammik.Localization.Interfaces
{
    public interface IDefaultResourceDictionarySettings
    {
        string DefaultResourcesDirectory { get; set; }
        string DefaultResourceFileNameFormat { get; set; }
        SystemLanguage DefaultLanguage { get; set; }
    }
}