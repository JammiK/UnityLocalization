using System;
using Jammik.Localization.Interfaces;
using UnityEngine;

namespace Jammik.Localization
{
    public class LocalizationManagerSettings : ILocalizationManagerSettings
    {
        public string DefaultResourcesDirectory { get; set; } = "Jammik/LocalizationManager/";
        public string DefaultResourceFileNameFormat { get; set; } = "Localization_{0}";
        public SystemLanguage DefaultLanguage { get; set; } = SystemLanguage.English;
        public UnknownKeyPolitics UnknownKeyPolitics { get; set; } = UnknownKeyPolitics.DisplayKey;
        public bool TestMode { get; set; } = false;
        public string DefaultString { get; set; } = string.Empty;
    }
}