using System.Collections.Generic;

namespace Jammik.Localization.Interfaces
{
    public interface ILocalizationDictionaryProvider
    {
        IDictionary<string, string> LoadDictionary();
    }
}