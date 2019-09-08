using System;
using System.Collections.Generic;
using Jammik.Localization.Interfaces;
using UnityEngine;

namespace Jammik.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        const string PackagePrefix = "[Localization Manager]:";
        
        readonly IDictionary<string, string> _dictionary;
        readonly ILocalizationManagerSettings _settings;
        readonly IUnknownKeyService _unknownKeyService;

        public LocalizationManager(ILocalizationManagerSettings settings,
            ILocalizationDictionaryProvider localizationDictionaryProvider)
        {
            _settings = settings ?? new LocalizationManagerSettings();
            var localizationDictionaryProviderInternal = localizationDictionaryProvider ??
                                                                              new DefaultResourcesLocalizationDictionaryProvider(_settings);

            _unknownKeyService = new UnknownKeyService(_settings.UnknownKeyPolitics, _settings.DefaultString);
            _dictionary = localizationDictionaryProviderInternal.LoadDictionary();
        }
        
        public string Get(string localizationKey)
        {
            if (_dictionary.ContainsKey(localizationKey))
            {
                return _dictionary[localizationKey];
            }

            if (_settings.TestMode)
            {
                Debug.LogError($"{PackagePrefix} Unable to find key '{localizationKey}'.");
            }

            return _unknownKeyService.GetUnknownKeyString(localizationKey);
        }
    }
}