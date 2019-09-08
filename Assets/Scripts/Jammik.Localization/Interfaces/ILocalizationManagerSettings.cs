using UnityEngine;

namespace Jammik.Localization.Interfaces
{
    public interface ILocalizationManagerSettings : IDefaultResourceDictionarySettings
    {
        
        UnknownKeyPolitics UnknownKeyPolitics { get; set; }
        bool TestMode { get; set; }
        string DefaultString { get; set; }
    }
}