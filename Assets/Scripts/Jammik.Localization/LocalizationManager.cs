using System;
using System.Collections.Generic;
using System.Data;
using Jammik.Localization.Interfaces;
using UnityEngine;

namespace Jammik.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        const string PackagePrefix = "[Localization Manager]:";
        
        readonly ILocalizationManagerSettings _settings;
        readonly IUnknownKeyService _unknownKeyService;
        readonly ILocalizationDictionaryProvider _localizationDictionaryProvider;

        IDictionary<string, string> _dictionary;
        
        public LocalizationManager(ILocalizationManagerSettings settings,
            ILocalizationDictionaryProvider localizationDictionaryProvider,
            ILanguageProvider languageProvider,
            ILocalizationDictionaryParser localizationDictionaryParser)
        {
            _settings = settings ?? new LocalizationManagerSettings();
            _localizationDictionaryProvider = localizationDictionaryProvider ??
                                              new DefaultResourcesLocalizationDictionaryProvider(_settings,
                                                  localizationDictionaryParser, languageProvider);

            _unknownKeyService = new UnknownKeyService(_settings.UnknownKeyPolitics, _settings.DefaultString);
            try
            {
                _dictionary = _localizationDictionaryProvider.LoadDictionary();
            }
            catch (DataException e)
            {
                if (_settings.TestMode)
                {
                    Debug.LogError($"{PackagePrefix} {e.Message}");
                }
            }
        }

        public void ChangeLanguage(SystemLanguage language)
        {
            _dictionary = _localizationDictionaryProvider.LoadDictionary(language);
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