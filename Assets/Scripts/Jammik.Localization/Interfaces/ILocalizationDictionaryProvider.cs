using System.Collections.Generic;
using UnityEngine;

namespace Jammik.Localization.Interfaces
{
    public interface ILocalizationDictionaryProvider
    {
        IDictionary<string, string> LoadDictionary();
        IDictionary<string, string> LoadDictionary(SystemLanguage language);
    }
}