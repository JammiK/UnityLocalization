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
        readonly string _fileNameFormat;
        readonly IDefaultResourceDictionarySettings _settings;
        readonly ILocalizationDictionaryParser _localizationDictionaryParser;
        
        string _directoryPath;

        public DefaultResourcesLocalizationDictionaryProvider(IDefaultResourceDictionarySettings settings,
            ILocalizationDictionaryParser localizationDictionaryParser,
            ILanguageProvider languageProvider = null)
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
                return new Dictionary<string, string>();
                throw new DataException($"Unable to find storage for localization.");
            }
            
            dictionary = LoadDictionaryInternal(_settings.DefaultLanguage);
            if (dictionary != null && dictionary.Any())
            {
                return dictionary;
            }

            throw new DataException($"Unable to find storage for localization.");
        }

        public IDictionary<string, string> LoadDictionary(SystemLanguage language)
        {
            var dictionary = LoadDictionaryInternal(language);
            return dictionary ?? LoadDictionary();
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