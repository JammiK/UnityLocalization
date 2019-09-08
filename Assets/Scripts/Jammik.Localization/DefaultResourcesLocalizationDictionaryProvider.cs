using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Jammik.Localization.Interfaces;
using UnityEngine;

namespace Jammik.Localization
{
    public class DefaultResourcesLocalizationDictionaryProvider : ILocalizationDictionaryProvider
    {
        readonly ILanguageProvider _languageProvider;
        string _directoryPath;
        readonly string _fileNameFormat;
        readonly IDefaultResourceDictionarySettings _settings;
        readonly ILocalizationDictionaryParser _localizationDictionaryParser;

        public DefaultResourcesLocalizationDictionaryProvider(IDefaultResourceDictionarySettings settings,
            ILanguageProvider languageProvider = null,
            ILocalizationDictionaryParser localizationDictionaryParser = null)
        {
            _languageProvider = languageProvider ?? new DefaultLanguageProvider();
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _localizationDictionaryParser = localizationDictionaryParser;

            _directoryPath = settings.DefaultResourcesDirectory;
            _fileNameFormat = settings.DefaultResourceFileNameFormat;

            UpdateDirectoryFormat();
        }

        public IDictionary<string, string> LoadDictionary()
        {
            var language = _languageProvider.GetLanguage();
            var dictionary = LoadDictionaryInternal(language);
            if (dictionary != null && dictionary.Any())
            {
                return dictionary;
            }

            if (language == _settings.DefaultLanguage)
            {
                throw new DataException($"Unable to find storage for localization.");
            }
            
            dictionary = LoadDictionaryInternal(_settings.DefaultLanguage);
            if (dictionary != null && dictionary.Any())
            {
                return dictionary;
            }

            throw new DataException($"Unable to find storage for localization.");
        }
        
        IDictionary<string, string> LoadDictionaryInternal(SystemLanguage language)
        {
            var path = GetPathForLanguage(language);
            if (string.IsNullOrEmpty(path))
            {
                throw new DataException($"Invalid empty path for localization resources.");
            }

            var textAsset = Resources.Load<TextAsset>(path);
            return textAsset == null ? 
                null 
                : _localizationDictionaryParser.Parse(textAsset.text);
        }

        string GetPathForLanguage(SystemLanguage language)
        {
            return $"{_directoryPath}{string.Format(_fileNameFormat, language.ToString())}";
        }

        void UpdateDirectoryFormat()
        {
            if (!(_directoryPath.EndsWith("/") || _directoryPath.EndsWith("\\")))
            {
                _directoryPath = $"{_directoryPath}/";
            }
        }
    }
}